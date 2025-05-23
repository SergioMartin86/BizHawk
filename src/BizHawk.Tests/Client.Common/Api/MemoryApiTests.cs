using System.Collections.Generic;

using BizHawk.Common;
using BizHawk.Client.Common;
using BizHawk.Emulation.Common;

namespace BizHawk.Tests.Client.Common.Api
{
	[TestClass]
	public sealed class MemoryApiTests
	{
		private IMemoryApi CreateDummyApi(byte[] memDomainContents)
			=> new MemoryApi(Console.WriteLine) //TODO capture and check for error messages?
			{
				MemoryDomainCore = new MemoryDomainList(new MemoryDomain[]
				{
					new MemoryDomainByteArray("ADomain", MemoryDomain.Endian.Little, memDomainContents, writable: true, wordSize: 1),
				}),
			};

		[TestMethod]
		public void TestBulkPeek()
		{
			var memApi = CreateDummyApi(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF });
			CollectionAssert.That.AreEqual(
				new byte[] { default, default, default },
				memApi.ReadByteRange(addr: -5, length: 3),
				"fully below lower boundary");
			CollectionAssert.That.AreEqual(
				new byte[] { default, default, 0x01, 0x23 },
				memApi.ReadByteRange(addr: -2, length: 4),
				"crosses lower boundary");
			CollectionAssert.That.AreEqual(
				new byte[] { default, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, default },
				memApi.ReadByteRange(addr: -1, length: 10),
				"crosses both boundaries");
			CollectionAssert.That.AreEqual(
				new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF },
				memApi.ReadByteRange(addr: 0, length: 8),
				"whole domain");
			CollectionAssert.That.AreEqual(
				new byte[] { 0x23, 0x45, 0x67, 0x89, 0xAB },
				memApi.ReadByteRange(addr: 1, length: 5),
				"strict contains");
			CollectionAssert.That.AreEqual(
				Array.Empty<byte>(),
				memApi.ReadByteRange(addr: 3, length: 0),
				"empty");
			CollectionAssert.That.AreEqual(
				new byte[] { 0xCD, 0xEF, default },
				memApi.ReadByteRange(addr: 6, length: 3),
				"crosses upper boundary");
			CollectionAssert.That.AreEqual(
				new byte[] { default, default, default, default },
				memApi.ReadByteRange(addr: 9, length: 4),
				"fully above upper boundary");
		}

		[TestMethod]
		public void TestBulkPoke()
		{
			void TestCase(IReadOnlyList<byte> expected, Action<IMemoryApi> action, string message)
			{
				var memDomainContents = new byte[8];
				action(CreateDummyApi(memDomainContents));
				CollectionAssert.That.AreEqual(expected, memDomainContents, message);
			}
			TestCase(
				new byte[8],
				memApi => memApi.WriteByteRange(-5, new byte[] { 0x01, 0x23, 0x45 }),
				"fully below lower boundary");
			TestCase(
				new byte[] { 0x45, 0x67, default, default, default, default, default, default },
				memApi => memApi.WriteByteRange(-2, new byte[] { 0x01, 0x23, 0x45, 0x67 }),
				"crosses lower boundary");
			TestCase(
				new byte[] { 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0xFE },
				memApi => memApi.WriteByteRange(-1, new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0xFE, 0xDC }),
				"crosses both boundaries");
			TestCase(
				new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF },
				memApi => memApi.WriteByteRange(0, new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF }),
				"whole domain");
			TestCase(
				new byte[] { default, 0x01, 0x23, 0x45, 0x67, 0x89, default, default },
				memApi => memApi.WriteByteRange(1, new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89 }),
				"strict contains");
			TestCase(
				new byte[8],
				memApi => memApi.WriteByteRange(3, Array.Empty<byte>()),
				"empty");
			TestCase(
				new byte[] { default, default, default, default, default, default, 0x01, 0x23 },
				memApi => memApi.WriteByteRange(6, new byte[] { 0x01, 0x23, 0x45 }),
				"crosses upper boundary");
			TestCase(
				new byte[8],
				memApi => memApi.WriteByteRange(9, new byte[] { 0x01, 0x23, 0x45, 0x67 }),
				"fully above upper boundary");
		}

		[TestMethod]
		public void TestHash()
		{
			var memApi = CreateDummyApi(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF });
			_ = Assert.Throws<ArgumentException>(
				() => memApi.HashRegion(addr: -5, count: 3),
				"fully below lower boundary");
			_ = Assert.Throws<ArgumentException>(
				() => memApi.HashRegion(addr: -2, count: 4),
				"crosses lower boundary");
			_ = Assert.Throws<ArgumentException>(
				() => memApi.HashRegion(addr: -1, count: 10),
				"crosses both boundaries");
			Assert.AreEqual(
				"55C53F5D490297900CEFA825D0C8E8E9532EE8A118ABE7D8570762CD38BE9818",
				memApi.HashRegion(addr: 0, count: 8),
				"whole domain");
			Assert.AreEqual(
				"233ABF8F525463C943A10F4DC3080B1BDA19D2EEAA4ACF4676F44689CED29232",
				memApi.HashRegion(addr: 1, count: 5),
				"strict contains");
			Assert.AreEqual(
				SHA256Checksum.EmptyFile,
				memApi.HashRegion(addr: 3, count: 0),
				"empty");
			_ = Assert.Throws<ArgumentException>(
				() => memApi.HashRegion(addr: 6, count: 3),
				"crosses upper boundary");
			_ = Assert.Throws<ArgumentException>(
				() => memApi.HashRegion(addr: 9, count: 4),
				"fully above upper boundary");
		}
	}
}

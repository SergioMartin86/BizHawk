CCFLAGS := \
	-I. \
	-I./core/prboom2/src \
	-Wfatal-errors \
	-DHAVE_CONFIG_H \
	-Dstricmp=strcasecmp \
	-Dstrnicmp=strncasecmp \
	-DNDEBUG \
	-ffast-math \
	-Wno-unused-function \
	-Wno-switch \
	-Wno-pointer-sign \
	-Wno-sign-compare \
	-Wno-unused-but-set-variable \
	-Wno-unused-variable \
	-Wno-deprecated-non-prototype

CXXFLAGS := \
	-I. \
	-I./core/prboom2/src \
	-Wfatal-errors \
	-DHAVE_CONFIG_H \
	-Dstricmp=strcasecmp \
	-Dstrnicmp=strncasecmp \
	-DNDEBUG \
	-ffast-math \
	-Wno-unused-function \
	-Wno-switch \
	-Wno-pointer-sign \
	-Wno-sign-compare \
	-Wno-unused-but-set-variable \
	-Wno-unused-variable 

LDFLAGS := 

TARGET := dsda.wbx

SRCS = \
	BizhawkInterface.c \
	core/prboom2/src/am_map.c \
	core/prboom2/src/doomdef.c \
	core/prboom2/src/doomstat.c \
	core/prboom2/src/dsda.c \
	core/prboom2/src/dsda/aim.c \
	core/prboom2/src/dsda/ambient.cpp \
	core/prboom2/src/dsda/analysis.c \
	core/prboom2/src/dsda/args.c \
	core/prboom2/src/dsda/brute_force.c \
	core/prboom2/src/dsda/build.c \
	core/prboom2/src/dsda/compatibility.c \
	core/prboom2/src/dsda/configuration.c \
	core/prboom2/src/dsda/console.c \
	core/prboom2/src/dsda/cr_table.c \
	core/prboom2/src/dsda/data_organizer.c \
	core/prboom2/src/dsda/death.c \
	core/prboom2/src/dsda/deh_hash.c \
	core/prboom2/src/dsda/demo.c \
	core/prboom2/src/dsda/destructible.c \
	core/prboom2/src/dsda/endoom.c \
	core/prboom2/src/dsda/episode.c \
	core/prboom2/src/dsda/excmd.c \
	core/prboom2/src/dsda/exdemo.c \
	core/prboom2/src/dsda/exhud.c \
	core/prboom2/src/dsda/features.c \
	core/prboom2/src/dsda/font.c \
	core/prboom2/src/dsda/game_controller.c \
	core/prboom2/src/dsda/ghost.c \
	core/prboom2/src/dsda/global.c \
	core/prboom2/src/dsda/hud_components/ammo_text.c \
	core/prboom2/src/dsda/hud_components/armor_text.c \
	core/prboom2/src/dsda/hud_components/attempts.c \
	core/prboom2/src/dsda/hud_components/base.c \
	core/prboom2/src/dsda/hud_components/big_ammo.c \
	core/prboom2/src/dsda/hud_components/big_armor.c \
	core/prboom2/src/dsda/hud_components/big_armor_text.c \
	core/prboom2/src/dsda/hud_components/big_artifact.c \
	core/prboom2/src/dsda/hud_components/big_health.c \
	core/prboom2/src/dsda/hud_components/big_health_text.c \
	core/prboom2/src/dsda/hud_components/color_test.c \
	core/prboom2/src/dsda/hud_components/command_display.c \
	core/prboom2/src/dsda/hud_components/composite_time.c \
	core/prboom2/src/dsda/hud_components/coordinate_display.c \
	core/prboom2/src/dsda/hud_components/event_split.c \
	core/prboom2/src/dsda/hud_components/fps.c \
	core/prboom2/src/dsda/hud_components/free_text.c \
	core/prboom2/src/dsda/hud_components/health_text.c \
	core/prboom2/src/dsda/hud_components/keys.c \
	core/prboom2/src/dsda/hud_components/level_splits.c \
	core/prboom2/src/dsda/hud_components/line_display.c \
	core/prboom2/src/dsda/hud_components/line_distance_tracker.c \
	core/prboom2/src/dsda/hud_components/line_tracker.c \
	core/prboom2/src/dsda/hud_components/local_time.c \
	core/prboom2/src/dsda/hud_components/map_coordinates.c \
	core/prboom2/src/dsda/hud_components/map_time.c \
	core/prboom2/src/dsda/hud_components/map_title.c \
	core/prboom2/src/dsda/hud_components/map_totals.c \
	core/prboom2/src/dsda/hud_components/message.c \
	core/prboom2/src/dsda/hud_components/minimap.c \
	core/prboom2/src/dsda/hud_components/mobj_tracker.c \
	core/prboom2/src/dsda/hud_components/null.c \
	core/prboom2/src/dsda/hud_components/player_tracker.c \
	core/prboom2/src/dsda/hud_components/ready_ammo_text.c \
	core/prboom2/src/dsda/hud_components/render_stats.c \
	core/prboom2/src/dsda/hud_components/secret_message.c \
	core/prboom2/src/dsda/hud_components/sector_tracker.c \
	core/prboom2/src/dsda/hud_components/speed_text.c \
	core/prboom2/src/dsda/hud_components/stat_totals.c \
	core/prboom2/src/dsda/hud_components/tracker.c \
	core/prboom2/src/dsda/hud_components/weapon_text.c \
	core/prboom2/src/dsda/id_list.c \
	core/prboom2/src/dsda/input.c \
	core/prboom2/src/dsda/key_frame.c \
	core/prboom2/src/dsda/map_format.c \
	core/prboom2/src/dsda/mapinfo.c \
	core/prboom2/src/dsda/mapinfo/doom.c \
	core/prboom2/src/dsda/mapinfo/doom/parser.cpp \
	core/prboom2/src/dsda/mapinfo/hexen.c \
	core/prboom2/src/dsda/mapinfo/legacy.c \
	core/prboom2/src/dsda/mapinfo/u.c \
	core/prboom2/src/dsda/memory.c \
	core/prboom2/src/dsda/messenger.c \
	core/prboom2/src/dsda/mobjinfo.c \
	core/prboom2/src/dsda/mouse.c \
	core/prboom2/src/dsda/msecnode.c \
	core/prboom2/src/dsda/music.c \
	core/prboom2/src/dsda/name.c \
	core/prboom2/src/dsda/options.c \
	core/prboom2/src/dsda/palette.c \
	core/prboom2/src/dsda/pause.c \
	core/prboom2/src/dsda/pclass.c \
	core/prboom2/src/dsda/playback.c \
	core/prboom2/src/dsda/preferences.c \
	core/prboom2/src/dsda/quake.c \
	core/prboom2/src/dsda/render_stats.c \
	core/prboom2/src/dsda/save.c \
	core/prboom2/src/dsda/scroll.c \
	core/prboom2/src/dsda/settings.c \
	core/prboom2/src/dsda/sfx.c \
	core/prboom2/src/dsda/skill_info.c \
	core/prboom2/src/dsda/skip.c \
	core/prboom2/src/dsda/sndinfo.c \
	core/prboom2/src/dsda/spawn_number.c \
	core/prboom2/src/dsda/split_tracker.c \
	core/prboom2/src/dsda/sprite.c \
	core/prboom2/src/dsda/state.c \
	core/prboom2/src/dsda/stretch.c \
	core/prboom2/src/dsda/text_color.c \
	core/prboom2/src/dsda/text_file.c \
	core/prboom2/src/dsda/thing_id.c \
	core/prboom2/src/dsda/time.c \
	core/prboom2/src/dsda/tracker.c \
	core/prboom2/src/dsda/tranmap.c \
	core/prboom2/src/dsda/udmf.cpp \
	core/prboom2/src/dsda/utility.c \
	core/prboom2/src/dsda/utility/string_view.c \
	core/prboom2/src/dsda/wad_stats.c \
	core/prboom2/src/dstrings.c \
	core/prboom2/src/d_deh.c \
	core/prboom2/src/d_items.c \
	core/prboom2/src/d_main.c \
	core/prboom2/src/e6y.c \
	core/prboom2/src/f_finale.c \
	core/prboom2/src/f_wipe.c \
	core/prboom2/src/g_game.c \
	core/prboom2/src/g_overflow.c \
	core/prboom2/src/heretic/d_main.c \
	core/prboom2/src/heretic/f_finale.c \
	core/prboom2/src/heretic/info.c \
	core/prboom2/src/heretic/in_lude.c \
	core/prboom2/src/heretic/level_names.c \
	core/prboom2/src/heretic/mn_menu.c \
	core/prboom2/src/heretic/sb_bar.c \
	core/prboom2/src/heretic/sounds.c \
	core/prboom2/src/hexen/a_action.c \
	core/prboom2/src/hexen/info.c \
	core/prboom2/src/hexen/f_finale.c \
	core/prboom2/src/hexen/h2_main.c \
	core/prboom2/src/hexen/in_lude.c \
	core/prboom2/src/hexen/p_acs.c \
	core/prboom2/src/hexen/p_anim.c \
	core/prboom2/src/hexen/p_things.c \
	core/prboom2/src/hexen/po_man.c \
	core/prboom2/src/hexen/sn_sonix.c \
	core/prboom2/src/hexen/sounds.c \
	core/prboom2/src/hexen/sv_save.c \
	core/prboom2/src/hu_lib.c \
	core/prboom2/src/hu_stuff.c \
	core/prboom2/src/info.c \
	core/prboom2/src/i_capture.c \
	core/prboom2/src/i_glob.c \
	core/prboom2/src/lprintf.c \
	core/prboom2/src/md5.c \
	core/prboom2/src/m_argv.c \
	core/prboom2/src/m_bbox.c \
	core/prboom2/src/m_cheat.c \
	core/prboom2/src/m_file.c \
	core/prboom2/src/m_menu.c \
	core/prboom2/src/m_misc.c \
	core/prboom2/src/m_random.c \
	core/prboom2/src/p_ceilng.c \
	core/prboom2/src/p_doors.c \
	core/prboom2/src/p_enemy.c \
	core/prboom2/src/p_floor.c \
	core/prboom2/src/p_genlin.c \
	core/prboom2/src/p_inter.c \
	core/prboom2/src/p_lights.c \
	core/prboom2/src/p_map.c \
	core/prboom2/src/p_maputl.c \
	core/prboom2/src/p_mobj.c \
	core/prboom2/src/p_plats.c \
	core/prboom2/src/p_pspr.c \
	core/prboom2/src/p_saveg.c \
	core/prboom2/src/p_setup.c \
	core/prboom2/src/p_sight.c \
	core/prboom2/src/p_spec.c \
	core/prboom2/src/p_switch.c \
	core/prboom2/src/p_telept.c \
	core/prboom2/src/p_tick.c \
	core/prboom2/src/p_user.c \
	core/prboom2/src/r_bsp.c \
	core/prboom2/src/r_data.c \
	core/prboom2/src/r_draw.c \
	core/prboom2/src/r_fps.c \
	core/prboom2/src/r_main.c \
	core/prboom2/src/r_patch.c \
	core/prboom2/src/r_plane.c \
	core/prboom2/src/r_segs.c \
	core/prboom2/src/r_sky.c \
	core/prboom2/src/r_things.c \
	core/prboom2/src/scanner.cpp \
	core/prboom2/src/sc_man.c \
	core/prboom2/src/smooth.c \
	core/prboom2/src/sounds.c \
	core/prboom2/src/st_lib.c \
	core/prboom2/src/st_stuff.c \
	core/prboom2/src/s_advsound.c \
	core/prboom2/src/s_sound.c \
	core/prboom2/src/tables.c \
	core/prboom2/src/umapinfo.cpp \
	core/prboom2/src/v_video.c \
	core/prboom2/src/wadtbl.c \
	core/prboom2/src/wi_stuff.c \
	core/prboom2/src/w_wad.c \
	core/prboom2/src/z_bmalloc.c \
	core/prboom2/src/z_zone.c \
	core/prboom2/src/d_client.c \
	core/prboom2/src/w_mmap.c \
	core/prboom2/src/memio.c \
	core/prboom2/src/mus2mid.c \
	core/prboom2/src/SDL/i_main.c \
	core/prboom2/src/SDL/i_sound.c \
	core/prboom2/src/SDL/i_sshot.c \
	core/prboom2/src/SDL/i_system.c \
	core/prboom2/src/SDL/i_video.c \
	core/prboom2/src/MUSIC/dumbplayer.c \
	core/prboom2/src/MUSIC/flplayer.c \
	core/prboom2/src/MUSIC/madplayer.c \
	core/prboom2/src/MUSIC/midifile.c \
	core/prboom2/src/MUSIC/opl.c \
	core/prboom2/src/MUSIC/opl3.c \
	core/prboom2/src/MUSIC/oplplayer.c \
	core/prboom2/src/MUSIC/opl_queue.c \
	core/prboom2/src/MUSIC/portmidiplayer.c \
	core/prboom2/src/MUSIC/vorbisplayer.c 

include ../common.mak

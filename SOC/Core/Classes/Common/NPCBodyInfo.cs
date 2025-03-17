using SOC.Classes.Lua;
using System.Linq;

namespace SOC.Classes.Common
{
    public static class NPCBodyInfo
    {
        // SubTypes
        public static string[] mafrSubTypes =
        {
            "PF_A",
            "PF_B",
            "PF_C",
        };

        public static string[] afghSubTypes =
        {
            "SOVIET_A",
            "SOVIET_B",
        };

        public static string[] mtbsSubTypes = {

        };

        public static string[] GetRegionSubTypes(int locId)
        {
            if (LoadAreas.isAfgh(locId))
                return afghSubTypes;
            else if (LoadAreas.isMafr(locId))
                return mafrSubTypes;
            else
                return mtbsSubTypes;
        }

        // Region Bodies
        public static string[] mafrBodies =
        {
            "DEFAULT",
            "pfs0_rfl_v00_a",
            "pfs0_rfl_v01_a",
            "pfs0_mcg_v00_a",
            "pfs0_snp_v00_a",
            "pfs0_rdo_v00_a",
            "pfs0_rfl_v00_b",
            "pfs0_rfl_v01_b",
            "pfs0_mcg_v00_b",
            "pfs0_snp_v00_b",
            "pfs0_rdo_v00_b",
            "pfs0_rfl_v00_c",
            "pfs0_rfl_v01_c",
            "pfs0_mcg_v00_c",
            "pfs0_snp_v00_c",
            "pfs0_rdo_v00_c",
            "pfs0_unq_v210",
            "pfs0_unq_v250",
            "pfs0_unq_v360",
            "pfs0_unq_v280",
            "pfs0_unq_v150",
            "pfs0_unq_v220",
            "pfs0_unq_v140",
            "pfs0_unq_v241",
            "pfs0_unq_v242",
            "pfs0_unq_v450",
            "pfs0_unq_v440",
            "pfs0_unq_v155",
            "svs0_dds0_v00",
        };

        public static string[] afghBodies =
        {
            "DEFAULT",
            "svs0_rfl_v00_a",
            "svs0_rfl_v01_a",
            "svs0_rfl_v02_a",
            "svs0_mcg_v00_a",
            "svs0_mcg_v01_a",
            "svs0_mcg_v02_a",
            "svs0_snp_v00_a",
            "svs0_rdo_v00_a",
            "svs0_rfl_v00_b",
            "svs0_rfl_v01_b",
            "svs0_rfl_v02_b",
            "svs0_mcg_v00_b",
            "svs0_mcg_v01_b",
            "svs0_mcg_v02_b",
            "svs0_snp_v00_b",
            "svs0_rdo_v00_b",
            "svs0_unq_v010",
            "svs0_unq_v080",
            "svs0_unq_v020",
            "svs0_unq_v040",
            "svs0_unq_v050",
            "svs0_unq_v060",
            "svs0_unq_v100",
            "svs0_unq_v070",
            "svs0_unq_v071",
            "svs0_unq_v072",
            "svs0_unq_v420",
            "svs0_unq_v009",
            "svs0_unq_v421",
            "pfs0_dds0_v00",
        };

        public static string[] mtbsBodies = {

        };

        public static string[] GetRegionBodies(int locId)
        {
            if (LoadAreas.isAfgh(locId))
                return afghBodies;
            else if (LoadAreas.isMafr(locId))
                return mafrBodies;
            else
                return mtbsBodies;
        }


        // IH hostage fv2's
        public static BodyInfoEntry[] BodyInfoArray = {
             new BodyInfoEntry(
                "AFGH_HOSTAGE",
                new LuaTableIdentifier("TppDefine", new LuaText("QUEST_BODY_ID_LIST"), new LuaText("AFGH_HOSTAGE_MALE")),
                "/Assets/tpp/parts/chara/prs/prs2_main0_def_v00_ih_hos.parts",
                "/Assets/tpp/pack/mission2/ih/prs2_main0_mdl.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "AFGH_HOSTAGE_FEMALE",
                new LuaTableIdentifier("TppDefine", new LuaText("QUEST_BODY_ID_LIST"), new LuaText("AFGH_HOSTAGE_FEMALE")),
                "/Assets/tpp/parts/chara/prs/prs3_main0_def_v00_ih_hos.parts",
                "/Assets/tpp/pack/mission2/ih/prs3_main0_mdl.fpk",
                true,
                false
                ),
            new BodyInfoEntry(
                "MAFR_HOSTAGE",
                new LuaTableIdentifier("TppDefine", new LuaText("QUEST_BODY_ID_LIST"), new LuaText("MAFR_HOSTAGE_MALE")),
                "/Assets/tpp/parts/chara/prs/prs5_main0_def_v00.parts",
                "/Assets/tpp/pack/mission2/ih/prs5_main0_mdl.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "MAFR_HOSTAGE_FEMALE",
                new LuaTableIdentifier("TppDefine", new LuaText("QUEST_BODY_ID_LIST"), new LuaText("MAFR_HOSTAGE_FEMALE")),
                "/Assets/tpp/parts/chara/prs/prs6_main0_def_v00_ih_hos.parts",
                "/Assets/tpp/pack/mission2/ih/prs6_main0_mdl.fpk",
                true,
                false
                ),
            new BodyInfoEntry(
                "DDS_RESEARCHER",
                new LuaTableIdentifier("TppDefine", new LuaText("QUEST_BODY_ID_LIST"), new LuaText("AFGH_HOSTAGE_MALE")),
                "/Assets/tpp/parts/chara/dds/ddr0_main0_def_v00_ih_hos.parts",
                "/Assets/tpp/pack/mission2/ih/ddr0_main0_mdl.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "DDS_RESEARCHER_FEMALE",
                new LuaTableIdentifier("TppDefine", new LuaText("QUEST_BODY_ID_LIST"), new LuaText("AFGH_HOSTAGE_FEMALE")),
                "/Assets/tpp/parts/chara/dds/ddr1_main0_def_v00_ih_hos.parts",
                "/Assets/tpp/pack/mission2/ih/ddr1_main0_mdl.fpk",
                true,
                false
                ),
            new BodyInfoEntry(
                "DRAB",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dds3_main0_v00")),
                "/Assets/tpp/parts/chara/dds/dds3_main0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_dd_soldier_wait.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "DRAB_FEMALE",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dds8_main0_v00")),
                "/Assets/tpp/parts/chara/dds/dds8_main0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_dd_soldier_wait.fpk",
                true,
                false
                ),
            new BodyInfoEntry(
                "TIGER",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dds5_main0_v00")),
                "/Assets/tpp/parts/chara/dds/dds5_enem0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_dd_soldier_attack.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "TIGER_FEMALE",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dds6_main0_v00")),
                "/Assets/tpp/parts/chara/dds/dds6_enef0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_dd_soldier_attack.fpk",
                true,
                false
                ),
            new BodyInfoEntry(
                "SNEAKING_SUIT",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dds4_enem0_def")),
                "/Assets/tpp/parts/chara/sna/sna4_enem0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_dd_soldier_sneak.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "SNEAKING_SUIT_FEMALE",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dds4_enef0_def")),
                "/Assets/tpp/parts/chara/sna/sna4_enef0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_dd_soldier_sneak.fpk",
                true,
                false
                ),
            new BodyInfoEntry(
                "BATTLE_DRESS",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dds5_enem0_def")),
                "/Assets/tpp/parts/chara/sna/sna5_enem0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_dd_soldier_btdrs.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "BATTLE_DRESS_FEMALE",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dds5_enem0_def")),
                "/Assets/tpp/parts/chara/sna/sna5_enef0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_dd_soldier_btdrs.fpk",
                true,
                false
                ),
            new BodyInfoEntry(
                "XOF_GASMASK",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("wss0_main0_v00")),
                "/Assets/tpp/parts/chara/wss/wss0_main0_def_v00_ih_sol.parts",
                "/Assets/tpp/pack/mission2/ih/wss0_main0_mdl.fpk",
                false,
                true
                ),
            new BodyInfoEntry(
                "XOF",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("wss4_main0_v00")),
                "/Assets/tpp/parts/chara/wss/wss4_main0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_xof_soldier.fpk",
                false,
                true
                ),
            new BodyInfoEntry(
                "MSF_TPP",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dds0_main1_v00")),
                "/Assets/tpp/parts/chara/dds/dds0_main1_def_v00_ih_hos.parts",
                "/Assets/tpp/pack/mission2/ih/dds0_main1_mdl.fpk",
                false,
                true
                ),
            new BodyInfoEntry(
                "DDS_PILOT2",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dds5_enem0_def")),
                "/Assets/tpp/parts/chara/dds/dds9_main0_def_v00_ih_hos.parts",
                "/Assets/tpp/pack/mission2/ih/dds9_main0_mdl.fpk",
                false,
                true
                ),
            new BodyInfoEntry(
                "WANDERING_MSF",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("pfs0_dds0_v00")),
                "/Assets/tpp/parts/chara/svs/svs0_main0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_afgh.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "SOVIET_BERET",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("svs0_unq_v010")),
                "/Assets/tpp/parts/chara/svs/svs0_main0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_afgh.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "SOVIET_HOODIE",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("svs0_unq_v060")),
                "/Assets/tpp/parts/chara/svs/svs0_main0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_afgh.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "SOVIET_SOLDIER",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("svs0_snp_v00_b")),
                "/Assets/tpp/parts/chara/svs/svs0_main0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_afgh.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "PF_C_BERET",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("pfs0_unq_v450")),
                "/Assets/tpp/parts/chara/pfs/pfs0_main0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_mafr.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "PF_SOLDIER",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("pfs0_snp_v00_a")),
                "/Assets/tpp/parts/chara/pfs/pfs0_main0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_mafr.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "SWIMWEAR_GW",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dlf_enem0_def")),
                "/Assets/tpp/parts/chara/dlf/dlf1_enem0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_dd_soldier_swim_suit.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "SWIMWEAR_GW_FEMALE",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dlf_enef0_def")),
                "/Assets/tpp/parts/chara/dlf/dlf0_enem0_def_f_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_dd_soldier_swim_suit.fpk",
                true,
                false
                ),
            new BodyInfoEntry(
                "SWIMWEAR_GOB",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dlg_enem0_def")),
                "/Assets/tpp/parts/chara/dlg/dlg1_enem0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_dd_soldier_swim_suit2.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "SWIMWEAR_GOB_FEMALE",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dlg_enef0_def")),
                "/Assets/tpp/parts/chara/dlg/dlg0_enem0_def_f_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_dd_soldier_swim_suit2.fpk",
                true,
                false
                ),
            new BodyInfoEntry(
                "SWIMWEAR_MEG",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dlh_enem0_def")),
                "/Assets/tpp/parts/chara/dlh/dlh1_enem0_def_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_dd_soldier_swim_suit3.fpk",
                false,
                false
                ),
            new BodyInfoEntry(
                "SWIMWEAR_MEG_FEMALE",
                new LuaTableIdentifier("TppEnemyBodyId", new LuaText("dlh_enef0_def")),
                "/Assets/tpp/parts/chara/dlh/dlh0_enem0_def_f_v00.parts",
                "/Assets/tpp/pack/mission2/common/mis_com_dd_soldier_swim_suit3.fpk",
                true,
                false
                ),
        };

        public static BodyInfoEntry GetBodyInfo(string bodyName)
        {
            foreach (BodyInfoEntry body in BodyInfoArray)
            {
                if (body.Name == bodyName)
                    return body;
            }
            return BodyInfoArray[0];
        }
    }

    public struct BodyInfoEntry
    {
        public string Name { get; set; }

        public LuaTableIdentifier gameId { get; set; }

        public string partsPath { get; set; }

        public string missionPackPath { get; set; }

        public bool isFemale { get; set; }

        public bool hasface { get; set; }

        public BodyInfoEntry(string name, LuaTableIdentifier id, string parts, string pack, bool female, bool face)
        {
            Name = name;
            gameId = id;
            partsPath = parts;
            missionPackPath = pack;
            isFemale = female;
            hasface = face;

        }
    }

}

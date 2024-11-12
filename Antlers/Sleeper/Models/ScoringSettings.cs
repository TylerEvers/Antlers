using System.Text.Json.Serialization;

namespace Antlers.Sleeper.Models
{
    public class ScoringSettings
    {
        //Passing Settings
        [JsonPropertyName("pass_yd")]
        public double PassYd { get; set; }
        [JsonPropertyName("pass_td")]
        public double PassTd { get; set; }
        [JsonPropertyName("pass_2pt")]
        public double Pass2pt { get; set; }
        [JsonPropertyName("bonus_pass_yd_300")]
        public double BonusPassYd300 { get; set; }
        [JsonPropertyName("bonus_pass_yd_400")]
        public double BonusPassYd400 { get; set; }
        [JsonPropertyName("pass_int")]
        public double PassInt { get; set; }

        //Rushing Settings
        [JsonPropertyName("rush_yd")]
        public double RushYd { get; set; }
        [JsonPropertyName("rush_td")]
        public double RushTd { get; set; }
        [JsonPropertyName("rush_2pt")]
        public double Rush2pt { get; set; }
        [JsonPropertyName("bonus_rush_yd_100")]
        public double BonusRushYd100 { get; set; }
        [JsonPropertyName("bonus_rush_yd_200")]
        public double BonusRushYd200 { get; set; }

        //Receiving Settings
        [JsonPropertyName("rec")]
        public double Rec { get; set; }
        [JsonPropertyName("rec_yd")]
        public double RecYd { get; set; }
        [JsonPropertyName("rec_td")]
        public double RecTd { get; set; }
        [JsonPropertyName("rec_2pt")]
        public double Rec2pt { get; set; }
        [JsonPropertyName("bonus_rec_yd_100")]
        public double BonusRecYd100 { get; set; }
        [JsonPropertyName("bonus_rec_yd_200")]
        public double BonusRecYd200 { get; set; }

        //IDP Settings
        [JsonPropertyName("idp_int")]
        public double IdpInt { get; set; }
        [JsonPropertyName("idp_safe")]
        public double IdpSafe { get; set; }
        [JsonPropertyName("idp_def_td")]
        public double IdpDefTd { get; set; }
        [JsonPropertyName("idp_ff")]
        public double IdpFf { get; set; }
        [JsonPropertyName("idp_fum_rec")]
        public double IdpFumRec { get; set; }
        [JsonPropertyName("idp_tkl")]
        public double IdpTackle { get; set; }
        [JsonPropertyName("idp_tkl_ast")]
        public double IdpTklAst { get; set; }
        [JsonPropertyName("idp_tkl_solo")]
        public double IdpTklSolo { get; set; }
        [JsonPropertyName("idp_tkl_loss")]
        public double IdpTklLoss { get; set; }
        [JsonPropertyName("idp_qb_hit")]
        public double IdpQbHit { get; set; }
        [JsonPropertyName("idp_sack")]
        public double IdpSack { get; set; }
        [JsonPropertyName("idp_pass_def")]
        public double IdpPassDef { get; set; }
        [JsonPropertyName("idp_pass_def_3p")]
        public double IdpPassDef3p { get; set; }
        [JsonPropertyName("idp_blk_kick")]
        public double IdpBlkKick { get; set; }

        //Fumble Settings
        [JsonPropertyName("fum")]
        public double Fum { get; set; }
        [JsonPropertyName("ff")]
        public double Ff { get; set; }
        [JsonPropertyName("st_ff")]
        public double StFf { get; set; }
        [JsonPropertyName("def_st_ff")]
        public double DefStFf { get; set; }
        [JsonPropertyName("fum_lost")]
        public double FumLost { get; set; }
        [JsonPropertyName("fum_rec")]
        public double FumRec { get; set; }
        [JsonPropertyName("st_fum_rec")]
        public double StFumRec { get; set; }
        [JsonPropertyName("def_st_fum_rec")]
        public double DefStFumRec { get; set; }
        [JsonPropertyName("fum_rec_td")]
        public double FumRecTd { get; set; }

        //Defense/Special Teams Settings
        [JsonPropertyName("pts_allow_0")]
        public double PtsAllow0 { get; set; }
        [JsonPropertyName("pts_allow_1_6")]
        public double PtsAllow1To6 { get; set; }
        [JsonPropertyName("pts_allow_7_13")]
        public double PtsAllow7To13 { get; set; }
        [JsonPropertyName("pts_allow_14_20")]
        public double PtsAllow14To20 { get; set; }
        [JsonPropertyName("pts_allow_21_27")]
        public double PtsAllow21To27 { get; set; }
        [JsonPropertyName("pts_allow_28_34")]
        public double PtsAllow28To34 { get; set; }
        [JsonPropertyName("pts_allow_35p")]
        public double PtsAllow35p { get; set; }
        [JsonPropertyName("int")]
        public double Int { get; set; }
        [JsonPropertyName("safe")]
        public double Safe { get; set; }
        [JsonPropertyName("sack")]
        public double Sack { get; set; }
        [JsonPropertyName("def_td")]
        public double DefTd { get; set; }
        [JsonPropertyName("st_td")]
        public double StTd { get; set; }
        [JsonPropertyName("def_st_td")]
        public double DefStTd { get; set; }
        [JsonPropertyName("blk_kick")]
        public double BlkKick { get; set; }
        [JsonPropertyName("bonus_tkl_10p")]
        public double BonusTkl10p { get; set; }

        //Kicker Settings
        [JsonPropertyName("xpm")]
        public double Xpm { get; set; }
        [JsonPropertyName("xpmiss")]
        public double Xpmiss { get; set; }
        [JsonPropertyName("fgm_0_19")]
        public double Fgm0To19 { get; set; }
        [JsonPropertyName("fgm_20_29")]
        public double Fgm20To29 { get; set; }
        [JsonPropertyName("fgm_30_39")]
        public double Fgm30To39 { get; set; }
        [JsonPropertyName("fgm_40_49")]
        public double Fgm40To49 { get; set; }
        [JsonPropertyName("fgm_50p")]
        public double Fgm50p { get; set; }
        [JsonPropertyName("fgmiss")]
        public double Fgmiss { get; set; }
    }
}

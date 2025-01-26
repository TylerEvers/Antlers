using Newtonsoft.Json;

namespace Antlers.Sleeper.Models
{
    public class ScoringSettings
    {
        //Passing Settings
        [JsonProperty("pass_yd")]
        public double PassYd { get; set; }
        [JsonProperty("pass_td")]
        public double PassTd { get; set; }
        [JsonProperty("pass_2pt")]
        public double Pass2pt { get; set; }
        [JsonProperty("bonus_pass_yd_300")]
        public double BonusPassYd300 { get; set; }
        [JsonProperty("bonus_pass_yd_400")]
        public double BonusPassYd400 { get; set; }
        [JsonProperty("pass_int")]
        public double PassInt { get; set; }

        //Rushing Settings
        [JsonProperty("rush_yd")]
        public double RushYd { get; set; }
        [JsonProperty("rush_td")]
        public double RushTd { get; set; }
        [JsonProperty("rush_2pt")]
        public double Rush2pt { get; set; }
        [JsonProperty("bonus_rush_yd_100")]
        public double BonusRushYd100 { get; set; }
        [JsonProperty("bonus_rush_yd_200")]
        public double BonusRushYd200 { get; set; }

        //Receiving Settings
        [JsonProperty("rec")]
        public double Rec { get; set; }
        [JsonProperty("rec_yd")]
        public double RecYd { get; set; }
        [JsonProperty("rec_td")]
        public double RecTd { get; set; }
        [JsonProperty("rec_2pt")]
        public double Rec2pt { get; set; }
        [JsonProperty("bonus_rec_yd_100")]
        public double BonusRecYd100 { get; set; }
        [JsonProperty("bonus_rec_yd_200")]
        public double BonusRecYd200 { get; set; }

        //IDP Settings
        [JsonProperty("idp_int")]
        public double IdpInt { get; set; }
        [JsonProperty("idp_safe")]
        public double IdpSafe { get; set; }
        [JsonProperty("idp_def_td")]
        public double IdpDefTd { get; set; }
        [JsonProperty("idp_ff")]
        public double IdpFf { get; set; }
        [JsonProperty("idp_fum_rec")]
        public double IdpFumRec { get; set; }
        [JsonProperty("idp_tkl")]
        public double IdpTackle { get; set; }
        [JsonProperty("idp_tkl_ast")]
        public double IdpTklAst { get; set; }
        [JsonProperty("idp_tkl_solo")]
        public double IdpTklSolo { get; set; }
        [JsonProperty("idp_tkl_loss")]
        public double IdpTklLoss { get; set; }
        [JsonProperty("idp_qb_hit")]
        public double IdpQbHit { get; set; }
        [JsonProperty("idp_sack")]
        public double IdpSack { get; set; }
        [JsonProperty("idp_pass_def")]
        public double IdpPassDef { get; set; }
        [JsonProperty("idp_pass_def_3p")]
        public double IdpPassDef3p { get; set; }
        [JsonProperty("idp_blk_kick")]
        public double IdpBlkKick { get; set; }

        //Fumble Settings
        [JsonProperty("fum")]
        public double Fum { get; set; }
        [JsonProperty("ff")]
        public double Ff { get; set; }
        [JsonProperty("st_ff")]
        public double StFf { get; set; }
        [JsonProperty("def_st_ff")]
        public double DefStFf { get; set; }
        [JsonProperty("fum_lost")]
        public double FumLost { get; set; }
        [JsonProperty("fum_rec")]
        public double FumRec { get; set; }
        [JsonProperty("st_fum_rec")]
        public double StFumRec { get; set; }
        [JsonProperty("def_st_fum_rec")]
        public double DefStFumRec { get; set; }
        [JsonProperty("fum_rec_td")]
        public double FumRecTd { get; set; }

        //Defense/Special Teams Settings
        [JsonProperty("pts_allow_0")]
        public double PtsAllow0 { get; set; }
        [JsonProperty("pts_allow_1_6")]
        public double PtsAllow1To6 { get; set; }
        [JsonProperty("pts_allow_7_13")]
        public double PtsAllow7To13 { get; set; }
        [JsonProperty("pts_allow_14_20")]
        public double PtsAllow14To20 { get; set; }
        [JsonProperty("pts_allow_21_27")]
        public double PtsAllow21To27 { get; set; }
        [JsonProperty("pts_allow_28_34")]
        public double PtsAllow28To34 { get; set; }
        [JsonProperty("pts_allow_35p")]
        public double PtsAllow35p { get; set; }
        [JsonProperty("int")]
        public double Int { get; set; }
        [JsonProperty("safe")]
        public double Safe { get; set; }
        [JsonProperty("sack")]
        public double Sack { get; set; }
        [JsonProperty("def_td")]
        public double DefTd { get; set; }
        [JsonProperty("st_td")]
        public double StTd { get; set; }
        [JsonProperty("def_st_td")]
        public double DefStTd { get; set; }
        [JsonProperty("blk_kick")]
        public double BlkKick { get; set; }
        [JsonProperty("bonus_tkl_10p")]
        public double BonusTkl10p { get; set; }

        //Kicker Settings
        [JsonProperty("xpm")]
        public double Xpm { get; set; }
        [JsonProperty("xpmiss")]
        public double Xpmiss { get; set; }
        [JsonProperty("fgm_0_19")]
        public double Fgm0To19 { get; set; }
        [JsonProperty("fgm_20_29")]
        public double Fgm20To29 { get; set; }
        [JsonProperty("fgm_30_39")]
        public double Fgm30To39 { get; set; }
        [JsonProperty("fgm_40_49")]
        public double Fgm40To49 { get; set; }
        [JsonProperty("fgm_50p")]
        public double Fgm50p { get; set; }
        [JsonProperty("fgmiss")]
        public double Fgmiss { get; set; }
    }
}

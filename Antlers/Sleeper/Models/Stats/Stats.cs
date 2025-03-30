using Newtonsoft.Json;

namespace Antlers.Sleeper.Models
{
    public class Stats
    {
        [JsonProperty("adp_2qb")]
        public float Adp2Qb { get; set; }

        [JsonProperty("adp_dynasty")]
        public float AdpDynasty { get; set; }

        [JsonProperty("adp_dynasty_2qb")]
        public float AdpDynasty2Qb { get; set; }

        [JsonProperty("adp_dynasty_half_ppr")]
        public float AdpDynastyHalfPpr { get; set; }

        [JsonProperty("adp_dynasty_ppr")]
        public float AdpDynastyPpr { get; set; }

        [JsonProperty("adp_dynasty_std")]
        public float AdpDynastyStd { get; set; }

        [JsonProperty("adp_half_ppr")]
        public float AdpHalfPpr { get; set; }

        [JsonProperty("adp_idp")]
        public float AdpIdp { get; set; }

        [JsonProperty("adp_ppr")]
        public float AdpPpr { get; set; }

        [JsonProperty("adp_rookie")]
        public float AdpRookie { get; set; }

        [JsonProperty("adp_std")]
        public float AdpStd { get; set; }

        [JsonProperty("rush_lng")]
        public float RushLng { get; set; }

        [JsonProperty("rush_td_lng")]
        public float RushTdLng { get; set; }

        [JsonProperty("pass_int")]
        public float PassInt { get; set; }

        [JsonProperty("pass_air_yd")]
        public float PassAirYd { get; set; }

        [JsonProperty("pos_rank_std")]
        public float PosRankStd { get; set; }

        [JsonProperty("gp")]
        public float GamesPlayed { get; set; }

        [JsonProperty("bonus_pass_yd_400")]
        public float BonusPassYd400 { get; set; }

        [JsonProperty("rank_half_ppr")]
        public int RankHalfPpr { get; set; }

        [JsonProperty("tm_def_snp")]
        public float TeamDefSnp { get; set; }

        [JsonProperty("gms_active")]
        public float GamesActive { get; set; }

        [JsonProperty("penalty")]
        public float Penalty { get; set; }

        [JsonProperty("pass_sack_yds")]
        public float PassSackYds { get; set; }

        [JsonProperty("rank_std")]
        public int RankStd { get; set; }

        [JsonProperty("pos_rank_half_ppr")]
        public float PosRankHalfPpr { get; set; }

        [JsonProperty("rush_td")]
        public float RushTd { get; set; }

        [JsonProperty("pass_ypc")]
        public float PassYpc { get; set; }

        [JsonProperty("pass_lng")]
        public float PassLng { get; set; }

        [JsonProperty("pass_rush_yd")]
        public float PassRushYd { get; set; }

        [JsonProperty("pts_std")]
        public float PtsStd { get; set; }

        [JsonProperty("tm_st_snp")]
        public float TeamStSnp { get; set; }

        [JsonProperty("pass_rtg")]
        public float PassRtg { get; set; }

        [JsonProperty("bonus_pass_cmp_25")]
        public float BonusPassCmp25 { get; set; }

        [JsonProperty("rush_fd")]
        public float RushFd { get; set; }

        [JsonProperty("pass_int_td")]
        public float PassIntTd { get; set; }

        [JsonProperty("pass_att")]
        public float PassAtt { get; set; }

        [JsonProperty("fum_lost")]
        public float FumLost { get; set; }

        [JsonProperty("rush_att")]
        public float RushAtt { get; set; }

        [JsonProperty("pass_rz_att")]
        public float PassRzAtt { get; set; }

        [JsonProperty("rush_ypa")]
        public float RushYpa { get; set; }

        [JsonProperty("gs")]
        public float Gs { get; set; }

        [JsonProperty("tm_off_snp")]
        public float TeamOffSnp { get; set; }

        [JsonProperty("pos_rank_ppr")]
        public float PosRankPpr { get; set; }

        [JsonProperty("rank_ppr")]
        public int RankPpr { get; set; }

        [JsonProperty("pass_sack")]
        public float PassSack { get; set; }

        [JsonProperty("penalty_yd")]
        public float PenaltyYd { get; set; }

        [JsonProperty("off_snp")]
        public float OffSnp { get; set; }

        [JsonProperty("pass_cmp")]
        public float PassCmp { get; set; }

        [JsonProperty("cmp_pct")]
        public float CmpPct { get; set; }

        [JsonProperty("rush_yac")]
        public float RushYac { get; set; }

        [JsonProperty("pts_half_ppr")]
        public float PtsHalfPpr { get; set; }

        [JsonProperty("pass_ypa")]
        public float PassYpa { get; set; }

        [JsonProperty("pass_cmp_40p")]
        public float PassCmp40p { get; set; }

        [JsonProperty("pass_fd")]
        public float PassFd { get; set; }

        [JsonProperty("pass_yd")]
        public float PassYd { get; set; }

        [JsonProperty("rush_rz_att")]
        public float RushRzAtt { get; set; }

        [JsonProperty("rush_rec_yd")]
        public float RushRecYd { get; set; }

        [JsonProperty("pass_td")]
        public float PassTd { get; set; }

        [JsonProperty("anytime_tds")]
        public float AnytimeTds { get; set; }

        [JsonProperty("bonus_fd_qb")]
        public float BonusFdQb { get; set; }

        [JsonProperty("rush_yd")]
        public float RushYd { get; set; }

        [JsonProperty("fum")]
        public float Fum { get; set; }

        [JsonProperty("pass_td_lng")]
        public float PassTdLng { get; set; }

        [JsonProperty("pts_ppr")]
        public float PtsPpr { get; set; }

        [JsonProperty("pass_inc")]
        public float PassInc { get; set; }

        [JsonProperty("rush_tkl_loss")]
        public float RushTklLoss { get; set; }
    }
}
using Newtonsoft.Json;

namespace Antlers.Sleeper.Models
{
    public class Stats
    {
        //TODO: Rename properties, keep Sleeper shorthand, add casing, and remove underscores OR use generaric name ie RushTd vs RushingTouchdowns?
        [JsonProperty("rush_lng")]
        public float rush_lng { get; set; }

        [JsonProperty("rush_td_lng")]
        public float rush_td_lng { get; set; }

        [JsonProperty("pass_int")]
        public float pass_int { get; set; }

        [JsonProperty("pass_air_yd")]
        public float pass_air_yd { get; set; }

        [JsonProperty("pos_rank_std")]
        public float pos_rank_std { get; set; }

        [JsonProperty("gp")]
        public float gp { get; set; }

        [JsonProperty("bonus_pass_yd_400")]
        public float bonus_pass_yd_400 { get; set; }

        [JsonProperty("rank_half_ppr")]
        public int rank_half_ppr { get; set; }

        [JsonProperty("tm_def_snp")]
        public float tm_def_snp { get; set; }

        [JsonProperty("gms_active")]
        public float gms_active { get; set; }

        [JsonProperty("penalty")]
        public float penalty { get; set; }

        [JsonProperty("pass_sack_yds")]
        public float pass_sack_yds { get; set; }

        [JsonProperty("rank_std")]
        public int rank_std { get; set; }

        [JsonProperty("pos_rank_half_ppr")]
        public float pos_rank_half_ppr { get; set; }

        [JsonProperty("rush_td")]
        public float rush_td { get; set; }

        [JsonProperty("pass_ypc")]
        public float pass_ypc { get; set; }

        [JsonProperty("pass_lng")]
        public float pass_lng { get; set; }

        [JsonProperty("pass_rush_yd")]
        public float pass_rush_yd { get; set; }

        [JsonProperty("pts_std")]
        public float pts_std { get; set; }

        [JsonProperty("tm_st_snp")]
        public float tm_st_snp { get; set; }

        [JsonProperty("pass_rtg")]
        public float pass_rtg { get; set; }

        [JsonProperty("bonus_pass_cmp_25")]
        public float bonus_pass_cmp_25 { get; set; }

        [JsonProperty("rush_fd")]
        public float rush_fd { get; set; }

        [JsonProperty("pass_int_td")]
        public float pass_int_td { get; set; }

        [JsonProperty("pass_att")]
        public float pass_att { get; set; }

        [JsonProperty("fum_lost")]
        public float fum_lost { get; set; }

        [JsonProperty("rush_att")]
        public float rush_att { get; set; }

        [JsonProperty("pass_rz_att")]
        public float pass_rz_att { get; set; }

        [JsonProperty("rush_ypa")]
        public float rush_ypa { get; set; }

        [JsonProperty("gs")]
        public float gs { get; set; }

        [JsonProperty("tm_off_snp")]
        public float tm_off_snp { get; set; }

        [JsonProperty("pos_rank_ppr")]
        public float pos_rank_ppr { get; set; }

        [JsonProperty("rank_ppr")]
        public int rank_ppr { get; set; }

        [JsonProperty("pass_sack")]
        public float pass_sack { get; set; }

        [JsonProperty("penalty_yd")]
        public float penalty_yd { get; set; }

        [JsonProperty("off_snp")]
        public float off_snp { get; set; }

        [JsonProperty("pass_cmp")]
        public float pass_cmp { get; set; }

        [JsonProperty("cmp_pct")]
        public float cmp_pct { get; set; }

        [JsonProperty("rush_yac")]
        public float rush_yac { get; set; }

        [JsonProperty("pts_half_ppr")]
        public float pts_half_ppr { get; set; }

        [JsonProperty("pass_ypa")]
        public float pass_ypa { get; set; }

        [JsonProperty("pass_cmp_40p")]
        public float pass_cmp_40p { get; set; }

        [JsonProperty("pass_fd")]
        public float pass_fd { get; set; }

        [JsonProperty("pass_yd")]
        public float pass_yd { get; set; }

        [JsonProperty("rush_rz_att")]
        public float rush_rz_att { get; set; }

        [JsonProperty("rush_rec_yd")]
        public float rush_rec_yd { get; set; }

        [JsonProperty("pass_td")]
        public float pass_td { get; set; }

        [JsonProperty("anytime_tds")]
        public float anytime_tds { get; set; }

        [JsonProperty("bonus_fd_qb")]
        public float bonus_fd_qb { get; set; }

        [JsonProperty("rush_yd")]
        public float rush_yd { get; set; }

        [JsonProperty("fum")]
        public float fum { get; set; }

        [JsonProperty("pass_td_lng")]
        public float pass_td_lng { get; set; }

        [JsonProperty("pts_ppr")]
        public float pts_ppr { get; set; }

        [JsonProperty("pass_inc")]
        public float pass_inc { get; set; }

        [JsonProperty("rush_tkl_loss")]
        public float rush_tkl_loss { get; set; }
    }
}

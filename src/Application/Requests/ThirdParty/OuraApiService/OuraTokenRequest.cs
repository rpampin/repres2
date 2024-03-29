﻿namespace Repres.Application.Requests.ThirdParty.OuraApiService
{
    public class OuraTokenRequest
    {
        public string grant_type { get => "authorization_code"; }
        public string code { get; set; }
        public string redirect_uri { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NBitcoin;

namespace BTCPayServer.Models.WalletViewModels
{
    public class SignWithSeedViewModel
    {
        public string OriginalPSBT  { get; set; }
        public string PayJoinEndpointUrl { get; set; }
        [Required]
        public string PSBT { get; set; }
        [Required][Display(Name = "BIP39 Seed (12/24 word mnemonic phrase) or HD private key (xprv...)")]
        public string SeedOrKey { get; set; }

        [Display(Name = "Optional seed passphrase")]
        [DataType(DataType.Password)]
        public string Passphrase { get; set; }

        public ExtKey GetExtKey(Network network)
        {
            ExtKey extKey = null;
            try
            {
                var mnemonic = new Mnemonic(SeedOrKey);
                extKey = mnemonic.DeriveExtKey(Passphrase);
            }
            catch (Exception)
            {
            }

            if (extKey == null)
            {
                try
                {
                    extKey = ExtKey.Parse(SeedOrKey, network);
                }
                catch (Exception)
                {
                }
            }
            return extKey;
        }
    }
}

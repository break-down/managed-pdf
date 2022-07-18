namespace BreakDown.ManagedPdf.Core.SharpZipLib.Zip
{
    /// <summary>
    /// Identifies the encryption algorithm used for an entry
    /// </summary>
    public enum EncryptionAlgorithm
    {
        /// <summary>
        /// No encryption has been used.
        /// </summary>
        None = 0,

        /// <summary>
        /// Encrypted using PKZIP 2.0 or 'classic' encryption.
        /// </summary>
        PkzipClassic = 1,

        /// <summary>
        /// DES encryption has been used.
        /// </summary>
        Des = 0x6601,

        /// <summary>
        /// RC2 encryption has been used for encryption.
        /// </summary>
        RC2 = 0x6602,

        /// <summary>
        /// Triple DES encryption with 168 bit keys has been used for this entry.
        /// </summary>
        TripleDes168 = 0x6603,

        /// <summary>
        /// Triple DES with 112 bit keys has been used for this entry.
        /// </summary>
        TripleDes112 = 0x6609,

        /// <summary>
        /// AES 128 has been used for encryption.
        /// </summary>
        Aes128 = 0x660e,

        /// <summary>
        /// AES 192 has been used for encryption.
        /// </summary>
        Aes192 = 0x660f,

        /// <summary>
        /// AES 256 has been used for encryption.
        /// </summary>
        Aes256 = 0x6610,

        /// <summary>
        /// RC2 corrected has been used for encryption.
        /// </summary>
        RC2Corrected = 0x6702,

        /// <summary>
        /// Blowfish has been used for encryption.
        /// </summary>
        Blowfish = 0x6720,

        /// <summary>
        /// Twofish has been used for encryption.
        /// </summary>
        Twofish = 0x6721,

        /// <summary>
        /// RC4 has been used for encryption.
        /// </summary>
        RC4 = 0x6801,

        /// <summary>
        /// An unknown algorithm has been used for encryption.
        /// </summary>
        Unknown = 0xffff
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CertificateShortcutProvider
{
    [XmlType("CertificateShortcutProviderKey")]
    public class CertificateShortcutProviderKey
    {
        public const int CurrentVersion = 1;

        [XmlAttribute]
        public int Version { get; set; }

        public byte[] Certificate { get; set; }

        /// <summary>
        /// Random key encrypted with the certificate.
        /// </summary>
        public byte[] EncryptedKey { get; set; }
        public byte[] IV { get; set; }
        /// <summary>
        /// User passphrase encrypted with the random key.
        /// </summary>
        public byte[] EncryptedPassphrase { get; set; }

        public CertificateShortcutProviderKey() { }
        public CertificateShortcutProviderKey(X509Certificate2 certificate, byte[] encryptedKey, byte[] iv, byte[] encryptedPassphrase)
        {
            Version = CurrentVersion;

            // public part only
            Certificate = certificate.Export(X509ContentType.Cert);

            EncryptedKey = encryptedKey;
            IV = iv;
            EncryptedPassphrase = encryptedPassphrase;
        }

        public X509Certificate2 ReadCertificate() => new X509Certificate2(Certificate);
    }
}

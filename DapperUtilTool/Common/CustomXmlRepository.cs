using Microsoft.AspNetCore.DataProtection.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DapperUtilTool.Common
{
   /// <summary>
   /// 
   /// </summary>
    public class CustomXmlRepository: IXmlRepository
    {
        private readonly string keyContent =
@"<?xml version='1.0' encoding='utf-8'?>
<key id='d0d4b07b-1b15-4e7f-b154-4da462e49de8' version='1'>
  <creationDate>2020-06-16T06:15:42.3784109Z</creationDate>
  <activationDate>2020-06-16T06:15:42.2153946Z</activationDate>
  <expirationDate>2020-09-14T06:15:42.2153946Z</expirationDate>
  <descriptor deserializerType='Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel.AuthenticatedEncryptorDescriptorDeserializer, Microsoft.AspNetCore.DataProtection, Version=3.1.4.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'>
    <descriptor>
      <encryption algorithm='AES_256_CBC' />
      <validation algorithm='HMACSHA256' />
      <masterKey p4:requiresEncryption='true' xmlns:p4='http://schemas.asp.net/2015/03/dataProtection'>
        <!-- Warning: the key below is in an unencrypted form. -->
      <value>4ipHWp+TtX1D60PeaiINnCqlSwVQJbjSW0gw/1PkHQ525B3boGWSqz9cyKCVwuRkYS8cDPwDNKebPTOuDy7BAg==</value>
      </masterKey>
    </descriptor>
  </descriptor>
</key>";

        public virtual IReadOnlyCollection<XElement> GetAllElements()
        {
            return GetAllElementsCore().ToList().AsReadOnly();
        }

        private IEnumerable<XElement> GetAllElementsCore()
        {
            yield return XElement.Parse(keyContent);
        }
        public virtual void StoreElement(XElement element, string friendlyName)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            StoreElementCore(element, friendlyName);
        }

        private void StoreElementCore(XElement element, string filename)
        {
        }
    }
}

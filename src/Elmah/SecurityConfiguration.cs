#region License, Terms and Author(s)
//
// ELMAH - Error Logging Modules and Handlers for ASP.NET
// Copyright (c) 2004-9 Atif Aziz. All rights reserved.
//
//  Author(s):
//
//      Atif Aziz, http://www.raboof.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

[assembly: Elmah.Scc("$Id: SecurityConfiguration.cs addb64b2f0fa 2012-03-07 18:50:16Z azizatif $")]

namespace Elmah
{
    #region Imports

    using System;
    using System.Collections;
    using System.Globalization;

    #endregion

    [ Serializable ]
    internal sealed class SecurityConfiguration
    {
        public static readonly SecurityConfiguration Default;

        private readonly bool _allowRemoteAccess;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _encryptionSalt;
        private readonly string _encryptionSecret;
        private readonly bool _requireLogin;
        private readonly int _blockFailedAttempts;

        private static readonly string[] _trues = new string[] { "true", "yes", "on", "1" };

        static SecurityConfiguration()
        {
            Default = new SecurityConfiguration((IDictionary) Configuration.GetSubsection("security"));
        }
        
        public SecurityConfiguration(IDictionary options)
        {
            _allowRemoteAccess = GetBoolean(options, "allowRemoteAccess");
            _userName = GetString(options, "userName");
            _password = GetString(options, "password");
            _requireLogin = GetBoolean(options, "requireLogin");
            _blockFailedAttempts = GetInteger(options, "blockFailedAttempts");
            _encryptionSalt = GetString(options, "encryptionSalt");
            _encryptionSecret = GetString(options, "encryptionSecret");

            if (_blockFailedAttempts == 0) _blockFailedAttempts = 3;
        }
        
        public bool AllowRemoteAccess
        {
            get { return _allowRemoteAccess; }
        }

        public string UserName
        {
            get { return _userName; }
        }

        public string EncryptionSalt
        {
            get { return _encryptionSalt; }
        }

        public string EncryptionSecret
        {
            get { return _encryptionSecret; }
        }
        public string Password
        {
            get { return _password; }
        }

        public bool RequireLogin
        {
            get { return _requireLogin; }
        }

        public int BlockFailedAttempts
        {
            get { return _blockFailedAttempts; }
        }

        private static bool GetBoolean(IDictionary options, string name)
        {
            string str = GetString(options, name).Trim().ToLower(CultureInfo.InvariantCulture);
            return Boolean.TrueString.Equals(StringTranslation.Translate(Boolean.TrueString, str, _trues));
        }

        private int GetInteger(IDictionary options, string name)
        {
            string str = GetString(options, name).Trim().ToLower(CultureInfo.InvariantCulture);
            int val;

            return int.TryParse(str, out val) ? val : 0;
        }

        private static string GetString(IDictionary options, string name)
        {
            Debug.Assert(name != null);

            if (options == null)
                return string.Empty;

            object value = options[name];

            if (value == null)
                return string.Empty;

            return Mask.NullString(value.ToString());
        }
    }
}
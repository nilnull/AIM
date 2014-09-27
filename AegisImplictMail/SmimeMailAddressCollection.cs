/*
 * Copyright (C)2014 Araz Farhang Dareshuri
 * This file is a part of Aegis Implicit Mail (AIM)
 * Aegis Implicit Mail is free software: 
 * you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.  
 * If not, see <http://www.gnu.org/licenses/>.
 *
 * If you need any more details please contact <a.farhang.d@gmail.com>
 * 
 * Aegis Implicit Mail is an implict ssl package to use mine/smime messages on implict ssl servers
 */

using System;
using System.Collections.ObjectModel;

namespace AegisImplicitMail
{
    /// <summary>
    /// Stores e-mail addresses that are associated with an e-mail message.
    /// </summary>
    public class SmimeMailAddressCollection
        : Collection<SmimeMailAddress>
    {
        # region Constructors

        # endregion

        # region Methods

        /// <summary>
        /// Adds a list of e-mail addresses to the collection.
        /// </summary>
        /// <param name="addresses">The addresses to add to the collection.  Multiple e-mail addressses must be separated with a comma.</param>
        public void Add(string addresses)
        {
            var parser = new System.Net.Mail.MailAddressCollection {addresses};

            foreach (System.Net.Mail.MailAddress address in parser)
            {
                Add(new SmimeMailAddress(address.Address));
            }
        }

        /// <summary>
        /// Inserts an e-mail address into the SmimeMailAddressCollection at the specified location.
        /// </summary>
        /// <param name="index">The location at which to insert the e-mail address.</param>
        /// <param name="item">The e-mail address to insert.</param>
        protected override void InsertItem(int index, SmimeMailAddress item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            base.InsertItem(index, item);
        }

        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the e-mail address element to be replaced.</param>
        /// <param name="item">An e-mail address which will replace the element in the collection.</param>
        protected override void SetItem(int index, SmimeMailAddress item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            base.SetItem(index, item);
        }

        # endregion
    }
}

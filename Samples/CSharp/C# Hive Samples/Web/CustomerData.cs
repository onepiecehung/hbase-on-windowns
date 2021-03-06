﻿using Syncfusion.ThriftHive.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebSampleBrowser
{
    public class CustomerData
    {
        public class PersonDetail
        {
            public string ContactId { get; set; }
            public string FullName { get; set; }
            public string Age { get; set; }
            public string EmailId { get; set; }
            public string PhoneNumber { get; set; }
            public string ModifiedDate { get; set; }
        }

       public static IList<PersonDetail> list(HqlDataReader reader)
        {
            IList<PersonDetail> results = null;
           //Declaring the fetch size
            reader.FetchSize = 12;

            //Fetches the given amount of data from the resultant data
            HiveResultSet result = reader.FetchResult();

            //Read each row from the fetched result
                foreach (HiveRecord row in result)
                {
                    HttpContext.Current.Session["Results"] = results = result.Select(rowvalue => new PersonDetail
                     {
                         ContactId = rowvalue["contactid"].ToString(),
                         FullName = rowvalue["fullname"].ToString(),
                         Age = rowvalue["age"].ToString(),
                         EmailId = rowvalue["emailaddress"].ToString(),
                         PhoneNumber = rowvalue["phoneno"].ToString(),
                         ModifiedDate = rowvalue["modifieddate"].ToString()
                     }).ToList();
                }
              
            
            return results;     
   
        }
    }
}
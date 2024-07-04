using System;

namespace ChatApi.model.dbModel
{
    public class DbDataClass
    {
        public String DbConnectionString { get; set; } = "mongodb+srv://abuelyazidsoftware:mahmoud2020@clone.7pm38gz.mongodb.net/?retryWrites=true&w=majority&appName=clone";
        public String DatabaseName { get; set; } = "ChatNet";
        public string CollectionName { get; set; } = "person";
        public DbDataClass() {
            this.DbConnectionString= "mongodb+srv://abuelyazidsoftware:mahmoud2020@clone.7pm38gz.mongodb.net/?retryWrites=true&w=majority&appName=clone";
            this.DatabaseName = "ChatNet";
            this.CollectionName = "person";


        }

    }
}

# DALs
[![Build status](https://ci.appveyor.com/api/projects/status/tq98cre3909s8b3r?svg=true)](https://ci.appveyor.com/project/TianyuanC/dals)
[![Coverage Status](https://coveralls.io/repos/TianyuanC/dals/badge.svg?branch=master)](https://coveralls.io/r/TianyuanC/dals?branch=master)
[![NuGet](https://img.shields.io/nuget/v/DALs.Core.svg)](https://www.nuget.org/packages/DALs.Core/)

Tired of writing the same block of data access layer code for each project, I extracted the common pieces and put tons of unit test as well as integration test coverage on those.

* Execute Stored Procedure using SqlTransaction, SqlConnection and SqlCommand 
* Access Restful Services using HttpClient
* Azure storages...

# Nuget

* PM> Install-Package DALs.Core -Version 1.0.6-beta -Pre

# Usage

Update/Insert via stored procedure

```cs
/// <summary>
/// update ad.
/// </summary>
/// <returns>Task&lt;System.Boolean&gt;.</returns>
public async Task<bool> UpdateAsync()
{
    var content = new SqlParameter("content", SqlDbType.VarChar) { Value = "Test" };
    var id = new SqlParameter("id", SqlDbType.Int) { Value = 1 };
    var config = new SqlConfiguration(ConnectionString, "UpdateAd") { SqlParameters = new List<SqlParameter> { content, id } };
    return await sqlClient.CommandAsync(config) > 0;
} 
```
Retrieve data via stored procedure

```cs
/// <summary>
/// Gets ads.
/// </summary>
/// <returns>Task&lt;IEnumerable&lt;Ad&gt;&gt;.</returns>
public async Task<IEnumerable<Ad>> GetAsync()
{
    var config = new SqlConfiguration(ConnectionString, Settings.GetAd, SprocMode.ExecuteReader);
    return await sqlClient.QueryAsync(config, AdsLoader);
}

/// <summary>
/// Ads loader.
/// </summary>
/// <param name="reader">The reader.</param>
/// <returns>IEnumerable&lt;Ad&gt;.</returns>
public static IEnumerable<Ad> AdsLoader(IDataReader reader)
{
    var ads = new List<Ad>();

    while (reader.Read())
    {
        var adId = reader.Get<int>("ID");
        var lastMod = reader.Get<DateTime>("ModificationDate");
        var content = reader.Get<string>("Content");
        var counter = reader.Get<long>("Counter");
        var ad = new Ad
        {
            AdID = adId,
            LastModificationDate = lastMod,
            Content = content,
            TestCounter = counter
        };
        ads.Add(ad);

    }
    return ads;
}
```

using System;
using System.Data.SqlClient;

namespace Federal.Model
{
	/// <summary>
	/// Login Model
	/// </summary>
	public class SecurityLoginModel : ModelBase
	{
		public class Ordinal
		{
			public int LoginType;
			public int Name;
			public int IsDisabled;
			public Ordinal(SqlDataReader r)
			{
				LoginType = r.GetOrdinal("LoginType");
				Name = r.GetOrdinal("Name");
				IsDisabled = r.GetOrdinal("IsDisabled");
			}
		}
		/// <summary>
		/// The constructor.
		/// </summary>
		public SecurityLoginModel(Ordinal ordinal, SqlDataReader r)
		{
			switch (r.Field<int>(ordinal.LoginType))
			{
				case 0: LoginType = SecurityLoginType.User; break;
				case 1: LoginType = SecurityLoginType.Group; break;
				case 2: LoginType = SecurityLoginType.SqlServer; break;
				case 3: LoginType = SecurityLoginType.Certificate; break;
				case 4: LoginType = SecurityLoginType.AsymmetricKey; break;
				default:
					throw new InvalidOperationException();
			}
			Name = r.Field<string>(ordinal.Name);
			IsEnabled = (!r.Field<bool>(ordinal.IsDisabled));
		}

		/// <summary>
		/// Gets or sets the type of the login.
		/// </summary>
		/// <value>The type of the login.</value>
		public SecurityLoginType LoginType { get; protected set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; protected set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is enable.
		/// </summary>
		/// <value><c>true</c> if this instance is enable; otherwise, <c>false</c>.</value>
		public bool IsEnabled { get; protected set; }

		#region Fetch
		public class Fetch : CollectionChunkedFetch<Fetch, Ordinal, SecurityLoginModel>
		{
			public Fetch()
				: base(
					(() => @"
SELECT 'Server[@Name=' + quotename(CAST(serverproperty(N'Servername') AS sysname),'''') + ']' + '/Login[@Name=' + quotename(log.name,'''') + ']' AS [Urn],
log.name AS [Name], CASE WHEN N'U' = log.type THEN 0 WHEN N'G' = log.type THEN 1 WHEN N'S' = log.type THEN 2 WHEN N'C' = log.type THEN 3 WHEN N'K' = log.type THEN 4 END AS [LoginType],
log.is_disabled AS [IsDisabled], log.create_date AS [CreateDate]
FROM sys.server_principals AS log
WHERE (log.type in ('U', 'G', 'S', 'C', 'K') AND log.principal_id not between 101 and 255 AND log.name <> N'##MS_AgentSigningCertificate##')
ORDER BY [Name] ASC"),
					(r => new Ordinal(r)),
					((ordinal, r) => new SecurityLoginModel(ordinal, r))
				) { }
		}
		#endregion
	}
}

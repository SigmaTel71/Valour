/*  Valour (TM) - A free and secure chat client
 *  Copyright (C) 2025-2026 Valour Software LLC
 *  This program is subject to the GNU Affero General Public license
 *  A copy of the license should be included - if not, see <http://www.gnu.org/licenses/>
 */

using System.Runtime.Serialization;

namespace Valour.Config.Configs;

public class DbConfig
{
    public static DbConfig Instance;

    public enum DbType
    {
        [EnumMember(Value = "PostgreSQL")]
        PostgreSQL,
        [EnumMember(Value = "MariaDB")]
        MariaDB,
        [EnumMember(Value = "MySQL")]
        MySQL
    };

    public DbType? Type { get; set; }

    public string Host { get; set; }

    public int? Port
    {
        get
        {
            if (field == null)
            {
                if (Type == DbType.MySQL || Type == DbType.MariaDB)
                    return 3306;
                else
                    return 5432;
            }

            return field;
        }
        set;
    }

    public string Password { get; set; }

    public string Username { get; set; }

    public string Database { get; set; }

    public DbConfig()
    {
        // Set main instance to the most recently created config
        Instance = this;
    }
}
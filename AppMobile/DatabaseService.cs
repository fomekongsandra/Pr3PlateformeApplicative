
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AppMobile
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Student> GetStudentByNameAsync(string nom, string prenom)
        {
            Student student = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                SqlCommand cmd = new SqlCommand("SELECT * FROM etudiant WHERE Nom = @Nom AND Prenom = @Prenom", conn);
                cmd.Parameters.AddWithValue("@Nom", nom);
                cmd.Parameters.AddWithValue("@Prenom", prenom);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        student = new Student
                        {
                            Id = reader.GetInt32(0),
                            Nom = reader.GetString(1),
                            Prenom = reader.GetString(2),
                            GroupeId = reader.GetInt32(3),
                            PromotionId = reader.GetInt32(4)
                        };
                    }
                }
            }

            return student;
        }

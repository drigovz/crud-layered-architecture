using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public class AlunoBLL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConSQLServer"].ConnectionString;

        /// <summary>
        /// Lista alunos
        /// </summary>
        /// <returns>Reotorna uma lista de alunos</returns>
        public IEnumerable<Aluno> GetAlunos()
        {
            List<Aluno> alunos = new List<Aluno>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("GetAlunos", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Aluno aluno = new Aluno();
                        aluno.Id = Convert.ToInt32(rdr["Id"]);
                        aluno.Nome = rdr["Nome"].ToString();
                        aluno.Email = rdr["Email"].ToString();
                        aluno.Idade = Convert.ToInt32(rdr["Idade"]);
                        aluno.DataInscricao = Convert.ToDateTime(rdr["DataInscricao"]);
                        aluno.Sexo = rdr["Sexo"].ToString();
                        alunos.Add(aluno);
                    }
                }
                return alunos;
            }
            catch
            {
                throw;
            }
        }

        public void IncluirAluno(Aluno aluno)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("IncluirAluno", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramNome = new SqlParameter();
                    paramNome.ParameterName = "@Nome";
                    paramNome.Value = aluno.Nome;
                    cmd.Parameters.Add(paramNome);

                    SqlParameter paramEmail = new SqlParameter();
                    paramEmail.ParameterName = "@Email";
                    paramEmail.Value = aluno.Email;
                    cmd.Parameters.Add(paramEmail);

                    SqlParameter paramIdade = new SqlParameter();
                    paramIdade.ParameterName = "@Idade";
                    paramIdade.Value = aluno.Idade;
                    cmd.Parameters.Add(paramIdade);

                    SqlParameter paramDataInscricao = new SqlParameter();
                    paramDataInscricao.ParameterName = "@DataInscricao";
                    paramDataInscricao.Value = aluno.DataInscricao;
                    cmd.Parameters.Add(paramDataInscricao);

                    SqlParameter paramSexo = new SqlParameter();
                    paramSexo.ParameterName = "@Sexo";
                    paramSexo.Value = aluno.Sexo;
                    cmd.Parameters.Add(paramSexo);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }


    }
}

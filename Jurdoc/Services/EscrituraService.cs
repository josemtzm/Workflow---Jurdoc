using Jurdoc.Api.Interface;
using Jurdoc.Api.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jurdoc.Api.Services
{
    public class EscrituraService : IEscrituraService
    {
        private readonly string _connectionString;
        public EscrituraService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("OracleDBConnection");
        }
        public IEnumerable<Escritura> GetEscrituras()
        {
            List<Escritura> EscrituraList = new List<Escritura>();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "Select * from Escritura";
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Escritura stu = new Escritura
                        {
                            IdEscritura = Convert.ToInt32(rdr["IdEscritura"]),
                            NumeroEscritura = DBNull.Value.Equals(rdr["NumeroEscritura"]) == false ? rdr["NumeroEscritura"].ToString() : "",
                            Solicitante = DBNull.Value.Equals(rdr["Solicitante"]) == false ? rdr["Solicitante"].ToString() : "",
                            Email = DBNull.Value.Equals(rdr["Email"]) == false ? rdr["Email"].ToString() : "",
                            FechaEscritura = DBNull.Value.Equals(rdr["FechaEscritura"]) == false ? Convert.ToDateTime(rdr["FechaEscritura"]) : DateTime.Now,
                            Observaciones = DBNull.Value.Equals(rdr["Observaciones"]) == false ? rdr["Observaciones"].ToString() : "",
                            Id_Tipo_Documento = DBNull.Value.Equals(rdr["Id_Tipo_Documento"]) == false ? Convert.ToInt32(rdr["Id_Tipo_Documento"]) : 0,
                            Id_Estatus = DBNull.Value.Equals(rdr["Id_Estatus"]) == false ? Convert.ToInt32(rdr["Id_Estatus"]) : 0
                        };
                        EscrituraList.Add(stu);
                    }
                    con.Close();
                }
            }
            return EscrituraList;
        }
        public Escritura GetEscritura(int eid)
        {
            Escritura Escritura = new Escritura();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "Select * from Escritura where IdEscritura=" + eid + "";
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Escritura stu = new Escritura
                        {
                            IdEscritura = Convert.ToInt32(rdr["IdEscritura"]),
                            NumeroEscritura = DBNull.Value.Equals(rdr["NumeroEscritura"]) == false ? rdr["NumeroEscritura"].ToString() : "",
                            Solicitante = DBNull.Value.Equals(rdr["Solicitante"]) == false ? rdr["Solicitante"].ToString() : "",
                            Email = DBNull.Value.Equals(rdr["Email"]) == false ? rdr["Email"].ToString() : "",
                            FechaEscritura = DBNull.Value.Equals(rdr["FechaEscritura"]) == false ? Convert.ToDateTime(rdr["FechaEscritura"]) : DateTime.Now,
                            Observaciones = DBNull.Value.Equals(rdr["Observaciones"]) == false ? rdr["Observaciones"].ToString() : "",
                            Id_Tipo_Documento = DBNull.Value.Equals(rdr["Id_Tipo_Documento"]) == false ? Convert.ToInt32(rdr["Id_Tipo_Documento"]) : 0,
                            Id_Estatus = DBNull.Value.Equals(rdr["Id_Estatus"]) == false ? Convert.ToInt32(rdr["Id_Estatus"]) : 0
                        };
                        Escritura = stu;
                    }
                    con.Close();
                }
            }
            return Escritura;
        }
        public void AddEscritura(Escritura Escritura)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "Insert into Escritura(IdEscritura, NumeroEscritura, Solicitante, Email, FechaEscritura, Observaciones, Id_Tipo_Documento, Id_Estatus)" +
                            "Values(" + Escritura.IdEscritura + ",'" + 
                            Escritura.NumeroEscritura + "','" + 
                            Escritura.Solicitante + "','" +
                            Escritura.Email + 
                            "', TO_DATE('" + Escritura.FechaEscritura + "', 'dd/MM/yyyy HH24:mi:ss'),'" + 
                            Escritura.Observaciones + 
                            "','18','10')"; 
                        //Escritura.Id_Tipo_Documento + "','" + Escritura.Id_Estatus + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public void EditEscritura(Escritura Escritura)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "Update Escritura " +
                            "Set NumeroEscritura='" + Escritura.NumeroEscritura +
                            "', Solicitante='" + Escritura.Solicitante +
                            "', Email='" + Escritura.Email +
                            "', FechaEscritura=TO_DATE('" + Escritura.FechaEscritura +
                            "', 'dd/MM/yyyy HH24:mi:ss')" +
                            ", Observaciones='" + Escritura.Observaciones +
                            "', Id_Tipo_Documento='" + Escritura.Id_Tipo_Documento +
                            "', Id_Estatus='" + Escritura.Id_Estatus +
                            "' where IdEscritura=" + Escritura.IdEscritura + "";
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public void DeleteEscritura(int IdEscritura)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "Delete from Escritura where IdEscritura=" + IdEscritura + "";
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

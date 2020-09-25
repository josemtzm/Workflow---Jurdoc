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
    public class DocumentoEscrituraService : IDocumentoEscrituraService
    {
        private readonly string _connectionString;
        public DocumentoEscrituraService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("OracleDBConnection");
        }

        public IEnumerable<DocumentoEscritura> GetDocEscrituras()
        {
            List<DocumentoEscritura> DocEscrituraList = new List<DocumentoEscritura>();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "Select * from Documento_Escritura";
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        DocumentoEscritura stu = new DocumentoEscritura
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Descripcion = rdr["Descripcion"].ToString(),
                            IdEscritura = Convert.ToInt32(rdr["IdEscritura"]),

                        };
                        DocEscrituraList.Add(stu);
                    }
                    con.Close();
                }
            }
            return DocEscrituraList;
        }
        public DocumentoEscritura GetDocEscritura(int eid)
        {
            DocumentoEscritura DocEscritura = new DocumentoEscritura();
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "Select * from Documento_Escritura where Id =" + eid + "";
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        DocumentoEscritura stu = new DocumentoEscritura
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Descripcion = rdr["Descripcion"].ToString(),
                            IdEscritura = Convert.ToInt32(rdr["IdEscritura"]),

                        };
                        DocEscritura = stu;
                    }
                    con.Close();
                }
            }
            return DocEscritura;
        }
        public void AddDocEscritura(DocumentoEscritura documentoEscritura)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "Insert into Documento_Escritura(Id, Descripcion, IdEscritura)" +
                            "Values(" + 
                            documentoEscritura.Id + ",'" + documentoEscritura.Descripcion + "','" + documentoEscritura.IdEscritura + "')";
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
        public void EditDocEscritura(DocumentoEscritura documentoEscritura)
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
                            "Set Id ='" + documentoEscritura.Id +
                            "', Descripcion ='" + documentoEscritura.Descripcion +
                            ", IdEscritura ='" + documentoEscritura.IdEscritura +
                            "' where Id=" + documentoEscritura.IdEscritura + "";
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
        public void DeleteDocEscritura(DocumentoEscritura documentoEscritura)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "Delete from Escritura where Id =" + documentoEscritura.Id + "";
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

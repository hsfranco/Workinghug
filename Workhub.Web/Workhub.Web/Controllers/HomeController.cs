using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workhub.Web.Models;

namespace Workhub.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Modelo de Entidades
        public WorkhubEntities db = new WorkhubEntities();
        #endregion

        #region Action Results
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload()
        {

            ViewBag.MensagemRetorno = "Selecione o arquivo que você deseja importar!";
            ViewBag.TipoMensagem = "alert alert-info";
            return View();
        }

        /// <summary>
        /// Este método tem como objetivo realizar o processamento dos arquivos selecionados para importação pelo usuário, ele ira salvar todos os dados no banco de dados do sistema.
        /// </summary>
        /// <param name="uploadPosted"></param>
        /// <returns>
        ///     Retorna mensagem de erro ou sucesso!
        /// </returns>
        [HttpPost]
        public ActionResult Upload(UploadFiles uploadPosted)
        {
            List<ArquivosImportados> ListaDeArquivosImportados = new List<ArquivosImportados>();

            try
            {
                if (ModelState.IsValid)
                {
                    if (uploadPosted.ArquivoSelecionado != null)
                    {
                        // Importa arquivo stream para leitor de textos
                        TextReader ArquivoImportado = new StreamReader(uploadPosted.ArquivoSelecionado.InputStream, System.Text.Encoding.Default);


                        string Linha = string.Empty;
                        int NumeroLinha = 0;

                        while (Linha != null)
                        {
                            //Incrementa o número da linha atual.
                            NumeroLinha++;

                            // Pega a linha atual do arquivo
                            Linha = ArquivoImportado.ReadLine();

                            // Caso linha seja nula ou seja primeira linha do arquivo continuar para próxima
                            if (Linha == null || NumeroLinha == 1)
                                continue;

                            // Separa as colunas por tab
                            string[] Colunas = Linha.Split(char.Parse("\t"));

                            // Atribui e normaliza os dados do arquivo para classe do entityFramework
                            ArquivosImportados oArquivosImportados = new ArquivosImportados();

                            oArquivosImportados.Comprador = Colunas[0];
                            oArquivosImportados.Descricao = Colunas[1];
                            oArquivosImportados.PrecoUnitario = double.Parse(Colunas[2] != null ? Colunas[2] : "0");
                            oArquivosImportados.Quantidade = int.Parse(Colunas[3] != null ? Colunas[3] : "0");
                            oArquivosImportados.Endereco = Colunas[4];
                            oArquivosImportados.Fornecedor = Colunas[5];

                            //Adiciona na lista de entidades e na lista de retorno para visualização
                            ListaDeArquivosImportados.Add(oArquivosImportados);
                        }

                        //Salva todos os dados no banco de dados
                        db.ArquivosImportados.AddRange(ListaDeArquivosImportados);
                        db.SaveChanges();

                        // Retorna lista de linhas importadas
                        ViewData["LinhasImportadas"] = ListaDeArquivosImportados;

                        ViewBag.MensagemRetorno = string.Format("Arquivo '{0}' importado com sucesso, foram importadas {1} linhas para a base de dados!",
                                                                uploadPosted.ArquivoSelecionado.FileName,
                                                                ListaDeArquivosImportados.Count);
                        ViewBag.TipoMensagem = "alert alert-success";
                    }
                    else
                    {
                        // Retorna mensagem para tela
                        ViewBag.MensagemRetorno = "Por favor, selecione um arquivo para importação!";
                        ViewBag.TipoMensagem = "alert alert-warning";
                    }
                }
                else
                {
                    // Retorna mensagem para tela
                    ViewBag.MensagemRetorno = "Por favor, selecione um arquivo para importação!";
                    ViewBag.TipoMensagem = "alert alert-warning";
                }
            }
            catch (Exception)
            {
                // Retorna mensagem para tela
                ViewBag.MensagemRetorno = "Arquivo selecionado fora do template!";
                ViewBag.TipoMensagem = "alert alert-danger";
            }

            return View("Upload");
        }
        #endregion
    }
}
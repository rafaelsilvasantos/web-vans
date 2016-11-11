using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebVansSite.Models;
using Microsoft.AspNet.Identity;
using WebVansSite.Models.ViewModels;
using System.Web.Script.Serialization;

namespace WebVansSite.Controllers
{
    [Authorize]
    public class MinhasSolicitacoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var viewModel = new MinhasSolicitacoesViewModel();
            var userID = User.Identity.GetUserId();

            var solicitacoesParaMim = db.Solicitacaos
                                      .Where(s => s.Van.Proprietario.Id == userID)
                                      .OrderByDescending(s => s.DataSolicitacao)
                                      .Select(s => new SolicitacaoViewModel
                                      {
                                          DataSolicitacao = s.DataSolicitacao,
                                          EmailProprietario = s.Van.Proprietario.Email,
                                          EmailSolicitante = s.Solicitante.Email,
                                          ID = s.ID,
                                          Marca = s.Van.Modelo.Marca.Nome,
                                          Modelo = s.Van.Modelo.Nome,
                                          CidadeEstado = s.Van.CidadesAtendidas.FirstOrDefault().Nome + "/" + s.Van.CidadesAtendidas.FirstOrDefault().Estado.Sigla,
                                          NomeProprietario = s.Van.Proprietario.UserProfileInfo.Nome,
                                          NomeSolicitante = s.Solicitante.UserProfileInfo.Nome,
                                          TelefoneProprietario = s.Van.Proprietario.UserProfileInfo.Telefone,
                                          TelefoneSolicitante = s.Solicitante.UserProfileInfo.Telefone,
                                          TipoServico = s.Van.TipoServico.Nome
                                      });

            viewModel.SolicitacoesParaMim = solicitacoesParaMim;

            var solicitacoesFeitasPorMim = db.Solicitacaos
                                      .Where(s => s.Solicitante.Id == userID)
                                      .OrderByDescending(s => s.DataSolicitacao)
                                      .Select(s => new SolicitacaoViewModel
                                      {
                                          DataSolicitacao = s.DataSolicitacao,
                                          EmailProprietario = s.Van.Proprietario.Email,
                                          EmailSolicitante = s.Solicitante.Email,
                                          ID = s.ID,
                                          Marca = s.Van.Modelo.Marca.Nome,
                                          Modelo = s.Van.Modelo.Nome,
                                          CidadeEstado = s.Van.CidadesAtendidas.FirstOrDefault().Nome + "/" + s.Van.CidadesAtendidas.FirstOrDefault().Estado.Sigla,
                                          NomeProprietario = s.Van.Proprietario.UserProfileInfo.Nome,
                                          NomeSolicitante = s.Solicitante.UserProfileInfo.Nome,
                                          TelefoneProprietario = s.Van.Proprietario.UserProfileInfo.Telefone,
                                          TelefoneSolicitante = s.Solicitante.UserProfileInfo.Telefone,
                                          TipoServico = s.Van.TipoServico.Nome
                                      });

            viewModel.SolicitacoesFeitasPorMim = solicitacoesFeitasPorMim;

            return View(viewModel);
        }

        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();

            var solicitacao = db.Solicitacaos.Find(id);

            if (solicitacao == null)
            {
                return HttpNotFound();
            }

            var viewModel = new DetalheSolicitacaoViewModel
            {
                Ano = solicitacao.Van.Ano,
                DataCadastro = solicitacao.Van.DataCadastro,
                Descricao = solicitacao.Descricao,
                ID = solicitacao.ID,
                Marca = solicitacao.Van.Modelo.Marca.Nome,
                Modelo = solicitacao.Van.Modelo.Nome,
                CidadeEstado = solicitacao.Van.CidadesAtendidas.FirstOrDefault().Nome + "/" + solicitacao.Van.CidadesAtendidas.FirstOrDefault().Estado.Sigla,
                Observacoes = solicitacao.Van.Observacoes,
                NomeProprietario = solicitacao.Van.Proprietario.UserProfileInfo.Nome,
                NomeSolicitante = solicitacao.Solicitante.UserProfileInfo.Nome,
                TelefoneProprietario = solicitacao.Van.Proprietario.UserProfileInfo.Telefone,
                TelefoneSolicitante = solicitacao.Solicitante.UserProfileInfo.Telefone,
                TipoCarga = solicitacao.Van.TipoCarga != null ? solicitacao.Van.TipoCarga.Nome : string.Empty,
                TipoLocomocao = solicitacao.Van.TipoLocomocao != null ? solicitacao.Van.TipoLocomocao.Nome : string.Empty,
                TipoServico = solicitacao.Van.TipoServico != null ? solicitacao.Van.TipoServico.Nome : string.Empty,
                TipoVan = solicitacao.Van.TipoVan != null ? solicitacao.Van.TipoVan.Nome : string.Empty,
                DataSolicitacao = solicitacao.DataSolicitacao,
                MinhaVan = solicitacao.Van.Proprietario.Id == userId,
                EmailProprietario = solicitacao.Van.Proprietario.Email,
                EmailSolicitante = solicitacao.Solicitante.Email
            };


            return View(viewModel);
        }
    }
}
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
using System.Net.Mail;
using WebVansSite.Helpers.Util;
using System.Text;

namespace WebVansSite.Controllers
{
    [Authorize]
    public class PesquisarVansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PesquisarVans
        public ActionResult Index()
        {
            var viewModel = new IndexPesquisarVanViewModel();

            viewModel.TipoServico = new SelectList(db.TipoServicos.Where(ts => ts.Ativo).ToList(), "Id", "Nome");
            viewModel.Estado = new SelectList(db.Estados.ToList(), "Id", "Nome");

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IndexPesquisarVanViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                var resultadoBusca = db.Vans
                                     .Include(v => v.TipoCarga)
                                     .Include(v => v.TipoLocomocao)
                                     .Include(v => v.TipoServico)
                                     .Include(v => v.TipoVan)
                                     .Include(v => v.Proprietario)
                                     .Where(v => v.Proprietario.Id != userId &&
                                                 v.TipoServico.Id == viewModel.TipoServicoId &&
                                                 v.CidadesAtendidas.FirstOrDefault().Estado.Id == viewModel.EstadoId &&

                                                 ((viewModel.CidadeId.HasValue && viewModel.CidadeId.Value != 0 && v.CidadesAtendidas.FirstOrDefault() != null && v.CidadesAtendidas.FirstOrDefault().Id == viewModel.CidadeId.Value) ||
                                                 (viewModel.CidadeId.HasValue && viewModel.CidadeId.Value == 0) ||
                                                 (!viewModel.CidadeId.HasValue)) &&

                                                 ((viewModel.TipoCargaId.HasValue && viewModel.TipoCargaId.Value != 0 && v.TipoCarga != null && v.TipoCarga.Id == viewModel.TipoCargaId.Value) ||
                                                 (viewModel.TipoCargaId.HasValue && viewModel.TipoCargaId.Value == 0) ||
                                                 (!viewModel.TipoCargaId.HasValue)) &&

                                                 ((viewModel.TipoLocomocaoId.HasValue && viewModel.TipoLocomocaoId.Value != 0 && v.TipoLocomocao != null && v.TipoLocomocao.Id == viewModel.TipoLocomocaoId.Value) ||
                                                 (viewModel.TipoLocomocaoId.HasValue && viewModel.TipoLocomocaoId.Value == 0) ||
                                                 (!viewModel.TipoLocomocaoId.HasValue)) &&
                                                 ((viewModel.TipoVanId.HasValue && viewModel.TipoVanId.Value != 0 && v.TipoVan != null && v.TipoVan.Id == viewModel.TipoVanId.Value) ||
                                                 (viewModel.TipoVanId.HasValue && viewModel.TipoVanId.Value == 0) ||
                                                 (!viewModel.TipoVanId.HasValue)))
                                     .Select(v => new ResultadoPesquisarVansViewModel
                                     {
                                         Ano = v.Ano,
                                         Id = v.ID,
                                         Marca = v.Modelo.Marca.Nome,
                                         Modelo = v.Modelo.Nome,
                                         TipoServico = v.TipoServico.Nome,
                                         Proprietario = v.Proprietario.UserProfileInfo.Nome,
                                         Telefone = v.Proprietario.UserProfileInfo.Telefone,
                                         CidadeEstado = v.CidadesAtendidas.FirstOrDefault().Nome + "/" + v.CidadesAtendidas.FirstOrDefault().Estado.Sigla
                                     }).ToList();

                TempData["ResultadoPesquisa"] = resultadoBusca;

                return RedirectToAction("ResultadoPesquisa");
            }

            return View(viewModel);
        }

        // GET: PesquisarVans
        public ActionResult ResultadoPesquisa()
        {
            var viewModel = TempData["ResultadoPesquisa"] as List<ResultadoPesquisarVansViewModel>;

            TempData["ResultadoPesquisa"] = viewModel;

            return View(viewModel);
        }

        // GET: PesquisarVans/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();

            VanModel vanModel = db.Vans
                                .Include(v => v.TipoCarga)
                                .Include(v => v.TipoLocomocao)
                                .Include(v => v.TipoServico)
                                .Include(v => v.TipoVan)
                                .FirstOrDefault(v => v.ID == id);

            if (vanModel == null)
            {
                return HttpNotFound();
            }

            var viewModel = new DetalhesVanViewModel
            {
                Ano = vanModel.Ano,
                DataCadastro = vanModel.DataCadastro,
                ID = vanModel.ID,
                Marca = vanModel.Modelo.Marca.Nome,
                Modelo = vanModel.Modelo.Nome,
                Observacoes = vanModel.Observacoes,
                TipoCarga = vanModel.TipoCarga != null ? vanModel.TipoCarga.Nome : string.Empty,
                TipoLocomocao = vanModel.TipoLocomocao != null ? vanModel.TipoLocomocao.Nome : string.Empty,
                TipoServico = vanModel.TipoServico != null ? vanModel.TipoServico.Nome : string.Empty,
                TipoVan = vanModel.TipoVan != null ? vanModel.TipoVan.Nome : string.Empty,
                Proprietario = vanModel.Proprietario.UserProfileInfo.Nome,
                Telefone = vanModel.Proprietario.UserProfileInfo.Telefone,
                CidadeEstado = vanModel.CidadesAtendidas.FirstOrDefault().Nome + "/" + vanModel.CidadesAtendidas.FirstOrDefault().Estado.Sigla
            };


            return View(viewModel);
        }

        // GET: PesquisarVans/Detalhes/5
        public ActionResult Solicitar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();

            VanModel vanModel = db.Vans
                                .Include(v => v.TipoCarga)
                                .Include(v => v.TipoLocomocao)
                                .Include(v => v.TipoServico)
                                .Include(v => v.TipoVan)
                                .FirstOrDefault(v => v.ID == id);

            if (vanModel == null)
            {
                return HttpNotFound();
            }

            var viewModel = new SolicitarVanViewModel
            {
                Ano = vanModel.Ano,
                DataCadastro = vanModel.DataCadastro,
                ID = vanModel.ID,
                Marca = vanModel.Modelo.Marca.Nome,
                Modelo = vanModel.Modelo.Nome,
                Observacoes = vanModel.Observacoes,
                TipoCarga = vanModel.TipoCarga != null ? vanModel.TipoCarga.Nome : string.Empty,
                TipoLocomocao = vanModel.TipoLocomocao != null ? vanModel.TipoLocomocao.Nome : string.Empty,
                TipoServico = vanModel.TipoServico != null ? vanModel.TipoServico.Nome : string.Empty,
                TipoVan = vanModel.TipoVan != null ? vanModel.TipoVan.Nome : string.Empty,
                Proprietario = vanModel.Proprietario.UserProfileInfo.Nome,
                Telefone = vanModel.Proprietario.UserProfileInfo.Telefone,
                CidadeEstado = vanModel.CidadesAtendidas.FirstOrDefault().Nome + "/" + vanModel.CidadesAtendidas.FirstOrDefault().Estado.Sigla
            };


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Solicitar(SolicitarVanViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var solicitacaoModel = new SolicitacaoModel
                {
                    DataSolicitacao = DateTime.Now,
                    Descricao = viewModel.Descricao,
                    Solicitante = db.Users.Find(User.Identity.GetUserId()),
                    Van = db.Vans.Find(viewModel.ID)
                };

                db.Solicitacaos.Add(solicitacaoModel);
                db.SaveChanges();

                var vanSolicitada = db.Vans.Find(viewModel.ID);
                var solicitante = db.Users.Find(User.Identity.GetUserId());

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Sua van foi solicitada. Seguem abaixo os dados da Solicitação:");
                sb.AppendLine(string.Empty);

                sb.AppendLine(string.Format("Data: {0}", DateTime.Now));
                sb.AppendLine(string.Format("Van: {0} - {1}", vanSolicitada.Modelo.Marca.Nome, vanSolicitada.Modelo.Nome));
                sb.AppendLine(string.Format("Placa: {0}", vanSolicitada.Placa));

                sb.AppendLine(string.Empty);
                sb.AppendLine(string.Format("Usuário: {0}", solicitante.UserProfileInfo.Nome));
                sb.AppendLine(string.Format("E-mail: {0}", solicitante.Email));
                sb.AppendLine(string.Format("CPF: {0}", solicitante.UserProfileInfo.CPF));
                sb.AppendLine(string.Format("CNPJ: {0}", solicitante.UserProfileInfo.CNPJ));
                sb.AppendLine(string.Format("Telefone: {0}", solicitante.UserProfileInfo.Telefone));

                sb.AppendLine(string.Empty);
                sb.AppendLine(string.Format("Descrição: {0}", viewModel.Descricao));

                MailHelper.EnviarEmail(vanSolicitada.Proprietario.Email, vanSolicitada.Proprietario.UserProfileInfo.Nome, "[WebVans] - Van Solicitada!", sb.ToString());

                viewModel.SolicitacaoConcluida = true;
            }

            return View(viewModel);
        }
    }
}
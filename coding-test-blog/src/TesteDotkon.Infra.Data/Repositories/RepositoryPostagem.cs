using Microsoft.EntityFrameworkCore;
using TesteDotkon.Domain.Entities;
using TesteDotkon.Domain.Interfaces;
using TesteDotkon.Infra.CrossCutting.IoC.interfaces;
using TesteDotkon.Infra.Data.Context;
using TesteDotkon.Infra.Data.Repositories.Base;

namespace TesteDotkon.Infra.Data.Repositories;

public class RepositoryPostagem(TesteDotkonContext context) : RepositoryBase<Postagem, TesteDotkonContext>(context), IRepositoryPostagem, IInjectScoped;
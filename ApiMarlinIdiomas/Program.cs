using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using Domain.IRepositories;
using Infrastructure.Repositories;
using Domain.IServices;
using Application.Services;
using Api.Controllers;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
    
                builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
                builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();
                builder.Services.AddScoped<IMatriculaRepository, MatriculaRepository>();
                builder.Services.AddScoped<IAlunoService, AlunoService>();
                builder.Services.AddScoped<ITurmaService, TurmaService>();
                builder.Services.AddScoped<IMatriculaService, MatriculaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

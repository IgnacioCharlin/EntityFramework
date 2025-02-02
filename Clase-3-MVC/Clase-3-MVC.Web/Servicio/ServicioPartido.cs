﻿using Clase_3_MVC.Web.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Clase_3_MVC.Web.Servicio
{
    public class ServicioPartido : IServicioPartido
    {

        Partidos partidos = new Partidos();
        Equipos equipos = new Equipos();


        public List<PartidoViewModel> ObtenerPartidos()
        {
            return partidos.GetPartidos();
        }
        public void AgregarNuevoPartido(PartidoViewModel partido)
        {
            partidos.addPartido(partido);
        }

        public List<PartidoViewModel> ConsultarFecha(IFormCollection collection)
        {

            List<PartidoViewModel> partidos = ObtenerPartidos();

            DateTime fechaElegida = Convert.ToDateTime(collection["Fecha"]);
            List<PartidoViewModel> partidosDeEsaFecha = new List<PartidoViewModel>();

            foreach (PartidoViewModel partido in partidos)
            {

                if (partido.Fecha.Year == fechaElegida.Year &&
                    partido.Fecha.Month == fechaElegida.Month &&
                    partido.Fecha.Day == fechaElegida.Day)
                {

                    partidosDeEsaFecha.Add(partido);

                }
            }

            if (partidosDeEsaFecha.Count == 0)
            {
                throw new NoExistePartidosParaEsaFechaException();
            }

            return partidosDeEsaFecha;

        }

        public void AgregarNuevoPartido(IFormCollection collection)


        {

            List<PartidoViewModel> partidos = ObtenerPartidos();

            String nombreLocal = collection["NombreLocal"];
            String nombreVisitante = collection["NombreVisitante"];



            EquipoViewModel equipoLocal = ObtenerEquipoLocal(nombreLocal);
            EquipoViewModel equipoVisitante = ObtenerEquipoVisitante(nombreVisitante);


            PartidoViewModel partidoNuevo = new PartidoViewModel();

            partidoNuevo.local = equipoLocal;
            partidoNuevo.visitante = equipoVisitante;
            partidoNuevo.resultado = "-/-";
            partidoNuevo.Lugar = collection["Estadio"];
            partidoNuevo.Fecha = Convert.ToDateTime(collection["fecha"]);
            partidoNuevo.Id = partidos.Count + 1;

            partidos.Add(partidoNuevo);
        }

        private EquipoViewModel ObtenerEquipoVisitante(string nombreVisitante)
        {


            foreach (var equipo in equipos.GetEquipos())
            {
                if (equipo.Nombre == nombreVisitante)
                {
                    return equipo;
                }

            }
            return null;



        }

        private EquipoViewModel ObtenerEquipoLocal(string nombreLocal)
        {

            foreach (var equipo in equipos.GetEquipos())
            {
                if (equipo.Nombre == nombreLocal)
                {
                    return equipo;
                }

            }
            return null;
        }






    }



}


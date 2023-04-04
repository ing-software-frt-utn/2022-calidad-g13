using Microsoft.VisualStudio.TestTools.UnitTesting;
using Negocio.Interfaces;
using Negocio.Modelos;
using Negocio.Repositorio;
using System;
using Moq;
using Negocio.Servicios;
using Negocio.Modelos.Enumeraciones;

namespace Prueba
{
    [TestClass]
    public class UnitTest1
    {

        private Mock<IRepoLinea> _mockRepoLinea;
        private Mock<IRepoOrdenProduccion> _mockRepoOP;
        private Mock<IRepoUsuario> _mockRepoUsuario;


        public void Setup()
        {
            _mockRepoLinea = new Mock<IRepoLinea>();
            _mockRepoOP = new Mock<IRepoOrdenProduccion>();
            _mockRepoUsuario = new Mock<IRepoUsuario>();
        }

        [TestMethod]
        public void CrearOPExitosamente()
        {
            // Configurar dependencias simuladas
            Setup();

            // Crear instancia del servicio de lógica de negocio y pasarle las dependencias simuladas
            var servicio = new Servicio_OP(_mockRepoOP.Object, _mockRepoLinea.Object, _mockRepoUsuario.Object);

            var op = new ModeloOP { Numero_OP = 1568, Num_linea = 1, Sku_modelo = 785, Codigo_color = 152 };
            var legajoUsuario = 1234;

            // Simular que el número de OP es único
            _mockRepoOP.Setup(repo => repo.ComprobarUnicidad(op.Numero_OP)).Returns(true);

            // Simular que la línea está disponible
            _mockRepoLinea.Setup(repo => repo.ComprobarEstado(op.Num_linea)).Returns(true);

            // Simular la búsqueda de la línea
            var lineaMock = new ModeloLinea { Estado = Estado_Linea.Disponible };
            _mockRepoLinea.Setup(repo => repo.BuscarLinea(op.Num_linea)).Returns(lineaMock);

            //Simular que el usuario está disponible
            _mockRepoUsuario.Setup(repo => repo.ComprobarDisponibilidad(legajoUsuario)).Returns(true);

            var resultado = servicio.CrearOP(op, legajoUsuario);

            /// Verificar que el estado de la línea se ha actualizado correctamente
            lineaMock.Estado = Estado_Linea.No_Disponible;
            _mockRepoLinea.Verify(repo => repo.ActualizarLinea(lineaMock), Times.Once);

            // Verificar el resultado de la operación y su mensaje
            Assert.IsTrue(resultado.exito);
            Assert.AreEqual("Orden de producción creada exitosamente.", resultado.mensaje);
        }


        [TestMethod]
        public void CrearOPFallaPorUsuarioNoDisponible()
        {
            // Configurar dependencias simuladas
            Setup();

            // Crear instancia del servicio de lógica de negocio y pasarle las dependencias simuladas
            var servicio = new Servicio_OP(_mockRepoOP.Object, _mockRepoLinea.Object, _mockRepoUsuario.Object);

            var op = new ModeloOP { Numero_OP = 1568, Num_linea = 1, Sku_modelo = 785, Codigo_color = 152 };
            var legajoUsuario = 1234;

            // Simular que el número de OP es único
            _mockRepoOP.Setup(repo => repo.ComprobarUnicidad(op.Numero_OP)).Returns(true);

            // Simular que la línea está disponible
            _mockRepoLinea.Setup(repo => repo.ComprobarEstado(op.Num_linea)).Returns(true);

            // Simular la búsqueda de la línea
            var lineaMock = new Mock<ModeloLinea>();
            lineaMock.SetupGet(l => l.Estado).Returns(Estado_Linea.Disponible);
            _mockRepoLinea.Setup(repo => repo.BuscarLinea(op.Num_linea)).Returns(lineaMock.Object);

            // Simular que el usuario no está disponible
            _mockRepoUsuario.Setup(repo => repo.ComprobarDisponibilidad(legajoUsuario)).Returns(false);

            var resultado = servicio.CrearOP(op, legajoUsuario);

            Assert.IsFalse(resultado.exito);
            Assert.AreEqual("Usted ya se encuentra asociado a una Orden de Producción", resultado.mensaje);

            // Verificar que no se llamó al método IniciarOP del repositorio de OP
            _mockRepoOP.Verify(repo => repo.IniciarOP(op), Times.Never);

            // Verificar que no se actualizó el estado de la línea
            lineaMock.VerifySet(l => l.Estado = Estado_Linea.No_Disponible, Times.Never);
            _mockRepoLinea.Verify(repo => repo.ActualizarLinea(lineaMock.Object), Times.Never);
        }
    }


}


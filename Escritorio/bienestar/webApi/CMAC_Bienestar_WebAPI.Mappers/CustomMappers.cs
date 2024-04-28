using System.Collections.Generic;
using AutoMapper;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_WebAPI.DTOs;

namespace CMAC_Bienestar_WebAPI.Mappers;

public class CustomMappers : Profile
{
	private readonly IMapper mapper;

	public CustomMappers()
	{
	}

	public CustomMappers(IMapper mapper)
	{
		this.mapper = mapper;
	}

	internal TallaDTO TallaVMToTallaDTO(TallaVM talla)
	{
		return ((IMapperBase)mapper).Map<TallaDTO>((object)talla);
	}

	internal List<TallaDTO> TallaVMListToTallaDTOList(List<TallaVM> tallas)
	{
		return ((IMapperBase)mapper).Map<List<TallaDTO>>((object)tallas);
	}

	internal List<TallaKardexDTO> TallaKardexVMListToTallaKardexDTOList(List<TallaKardexVM> tallas)
	{
		return ((IMapperBase)mapper).Map<List<TallaKardexDTO>>((object)tallas);
	}

	internal UniformeVM UniformeDTOToUniformeVM(UniformeDTOCreate uniforme)
	{
		return ((IMapperBase)mapper).Map<UniformeVM>((object)uniforme);
	}

	internal UniformeVM UniformeDTOToUniformeVM(UniformeDTOUpdate uniforme)
	{
		return ((IMapperBase)mapper).Map<UniformeVM>((object)uniforme);
	}

	internal UniformeDTOOut UniformeVMToUniformeDTO(UniformeVM uniforme)
	{
		return new UniformeDTOOut
		{
			IdUniforme = uniforme.IdUniforme,
			Nombre = uniforme.Nombre,
			SKU = uniforme.SKU,
			Genero = uniforme.Genero,
			GUIDImagen = uniforme.GUIDImagen,
			Detalle = uniforme.Detalle,
			GUIDDetalle = uniforme.GUIDDetalle,
			Estado = uniforme.Estado,
			Precio = uniforme.Precio,
			TallaEstandar = TallaVMToTallaDTO(uniforme.TallaEstandar),
			Tallas = TallaVMListToTallaDTOList(uniforme.Tallas)
		};
	}

	internal UniformeKardexDTOOut UniformeKardexVMToUniformeKardexDTO(UniformeKardexVM uniforme)
	{
		return new UniformeKardexDTOOut
		{
			IdUniforme = uniforme.IdUniforme,
			Nombre = uniforme.Nombre,
			Tallas = TallaKardexVMListToTallaKardexDTOList(uniforme.Tallas)
		};
	}

	internal UniformePedidoDTOOut UniformeVMToUniformePedidoDTO(UniformeVM uniforme)
	{
		return new UniformePedidoDTOOut
		{
			IdUniforme = uniforme.IdUniforme,
			Nombre = uniforme.Nombre,
			SKU = uniforme.SKU,
			Genero = uniforme.Genero,
			GUIDImagen = uniforme.GUIDImagen,
			Detalle = uniforme.Detalle,
			GUIDDetalle = uniforme.GUIDDetalle,
			Estado = uniforme.Estado,
			Precio = uniforme.Precio,
			Item = uniforme.Item,
			TallaEstandar = TallaVMToTallaDTO(uniforme.TallaEstandar),
			Tallas = TallaVMListToTallaDTOList(uniforme.Tallas)
		};
	}

	internal List<UniformeDTOOut> UniformeVMListToUniformeDTOList(List<UniformeVM> uniformes)
	{
		List<UniformeDTOOut> list = new List<UniformeDTOOut>();
		foreach (UniformeVM uniforme in uniformes)
		{
			list.Add(UniformeVMToUniformeDTO(uniforme));
		}
		return list;
	}

	internal List<UniformeKardexDTOOut> UniformeKardexVMListToUniformeKardexDTOList(List<UniformeKardexVM> uniformes)
	{
		List<UniformeKardexDTOOut> list = new List<UniformeKardexDTOOut>();
		foreach (UniformeKardexVM uniforme in uniformes)
		{
			list.Add(UniformeKardexVMToUniformeKardexDTO(uniforme));
		}
		return list;
	}

	internal List<UniformePedidoDTOOut> UniformeVMListToUniformePedidoDTOList(List<UniformeVM> uniformes)
	{
		List<UniformePedidoDTOOut> list = new List<UniformePedidoDTOOut>();
		foreach (UniformeVM uniforme in uniformes)
		{
			list.Add(UniformeVMToUniformePedidoDTO(uniforme));
		}
		return list;
	}

	internal UniformeGrupoDTOOut UniformeGrupoVMToUniformeGrupoDTOOut(UniformeVM uniforme)
	{
		return new UniformeGrupoDTOOut
		{
			IdUniforme = uniforme.IdUniforme,
			Nombre = uniforme.Nombre,
			SKU = uniforme.SKU,
			Genero = uniforme.Genero,
			GUIDImagen = uniforme.GUIDImagen,
			Detalle = uniforme.Detalle,
			GUIDDetalle = uniforme.GUIDDetalle,
			Estado = uniforme.Estado,
			Item = uniforme.Item,
			TallaEstandar = TallaVMToTallaDTO(uniforme.TallaEstandar),
			Tallas = TallaVMListToTallaDTOList(uniforme.Tallas)
		};
	}

	internal List<UniformeGrupoDTOOut> UniformeGrupoVMListToUniformeGrupoDTOList(List<UniformeVM> uniformes)
	{
		List<UniformeGrupoDTOOut> list = new List<UniformeGrupoDTOOut>();
		foreach (UniformeVM uniforme in uniformes)
		{
			list.Add(UniformeGrupoVMToUniformeGrupoDTOOut(uniforme));
		}
		return list;
	}

	internal UniformeGrupoVM UniformeGrupoDTOToUniformeGrupoVM(UniformeGrupoDTOIn uniformeGrupo)
	{
		return ((IMapperBase)mapper).Map<UniformeGrupoVM>((object)uniformeGrupo);
	}

	internal List<UniformeGrupoVM> UniformeGrupoDTOListToUniformeGrupoVMList(List<UniformeGrupoDTOIn> uniformeGrupos)
	{
		List<UniformeGrupoVM> list = new List<UniformeGrupoVM>();
		foreach (UniformeGrupoDTOIn uniformeGrupo in uniformeGrupos)
		{
			list.Add(UniformeGrupoDTOToUniformeGrupoVM(uniformeGrupo));
		}
		return list;
	}

	internal GrupoVM GrupoDTOToGrupoVM(GrupoDTOCreate grupo)
	{
		return new GrupoVM
		{
			Nombre = grupo.Nombre,
			Genero = grupo.Genero,
			UniformeGrupo = UniformeGrupoDTOListToUniformeGrupoVMList(grupo.UniformeGrupo),
			GrupoEntidades = GrupoEntidadDTOToGrupoEntidadVM(grupo.GrupoEntidades)
		};
	}

	internal GrupoVM GrupoDTOToGrupoVM(GrupoDTOUpdate grupo)
	{
		return new GrupoVM
		{
			Nombre = grupo.Nombre,
			Genero = grupo.Genero,
			UniformeGrupo = UniformeGrupoDTOListToUniformeGrupoVMList(grupo.UniformeGrupo),
			GrupoEntidades = GrupoEntidadDTOToGrupoEntidadVM(grupo.GrupoEntidades)
		};
	}

	internal GrupoDTOOut GrupoVMToGrupoDTO(GrupoVM grupo)
	{
		return new GrupoDTOOut
		{
			IdGrupo = grupo.IdGrupo,
			Nombre = grupo.Nombre,
			Genero = grupo.Genero,
			Estado = grupo.Estado,
			Uniformes = UniformeGrupoVMListToUniformeGrupoDTOList(grupo.Uniformes),
			GrupoEntidades = GrupoEntidadVMToGrupoEntidadDTO(grupo.GrupoEntidades)
		};
	}

	internal List<GrupoEntidadDTOOut> GrupoEntidadVMToGrupoEntidadDTO(List<GrupoEntidadVM> grupoEntidad)
	{
		return ((IMapperBase)mapper).Map<List<GrupoEntidadDTOOut>>((object)grupoEntidad);
	}

	internal List<GrupoEntidadVM> GrupoEntidadDTOToGrupoEntidadVM(List<GrupoEntidadDTOIn> grupoEntidad)
	{
		return ((IMapperBase)mapper).Map<List<GrupoEntidadVM>>((object)grupoEntidad);
	}

	internal List<GrupoDTOOut> GrupoVMListToGrupoDTOList(List<GrupoVM> grupos)
	{
		List<GrupoDTOOut> list = new List<GrupoDTOOut>();
		foreach (GrupoVM grupo in grupos)
		{
			list.Add(GrupoVMToGrupoDTO(grupo));
		}
		return list;
	}

	public RolDTOOut RolVMToRolDTO(RolVM rol)
	{
		return new RolDTOOut
		{
			IdRol = rol.IdRol,
			Nombre = rol.Nombre
		};
	}

	public List<RolDTOOut> RolVMListToRolDTOList(List<RolVM> roles)
	{
		List<RolDTOOut> list = new List<RolDTOOut>();
		foreach (RolVM role in roles)
		{
			list.Add(RolVMToRolDTO(role));
		}
		return list;
	}

	internal RolVM RolDTOToRolVM(RolDTOCreate rol)
	{
		return ((IMapperBase)mapper).Map<RolVM>((object)rol);
	}

	public UsuarioVM UsuarioDTOToUsuarioVM(UsuarioDtoIn usuario)
	{
		return ((IMapperBase)mapper).Map<UsuarioVM>((object)usuario);
	}

	public List<UsuarioDTOOut> UsuarioVMListToUsuarioDTOList(List<UsuarioVM> usuarios)
	{
		List<UsuarioDTOOut> list = new List<UsuarioDTOOut>();
		foreach (UsuarioVM usuario in usuarios)
		{
			list.Add(UsuarioVMToUsuarioDTO(usuario));
		}
		return list;
	}

	public UsuarioDTOOut UsuarioVMToUsuarioDTO(UsuarioVM usuario)
	{
		return new UsuarioDTOOut
		{
			IdUsuario = usuario.IdUsuario,
			Nombre = usuario.Nombre,
			NombreUsuario = usuario.NombreUsuario,
			Dni = usuario.Dni,
			Email = usuario.Email,
			IdTrabajador = (usuario.IdColaborador.HasValue ? ("1-" + usuario.IdColaborador) : ("2-" + usuario.employee_id)),
			employee_id = usuario.employee_id,
			Puesto = usuario.Puesto,
			IdRol = usuario.IdRol,
			Rol = usuario.Rol,
			ActiveDirectory = usuario.ActiveDirectory,
			Grupo = usuario.Grupo,
			IdGrupo = usuario.IdGrupo
		};
	}

	public TrabajadorDTOOut TrabajadorVMToTrabajadorDTO(TrabajadorVM trabajador)
	{
		return new TrabajadorDTOOut
		{
			IdTrabajador = (trabajador.IdColaborador.HasValue ? ("1-" + trabajador.IdColaborador) : ("2-" + trabajador.employee_id)),
			IdColaborador = trabajador.IdColaborador,
			employee_id = trabajador.employee_id,
			Nombre = trabajador.Nombre,
			NombreUsuario = trabajador.NombreUsuario,
			Dni = trabajador.Dni,
			Email = ((!string.IsNullOrEmpty(trabajador.NombreUsuario)) ? (trabajador.NombreUsuario + "@cajaarequipa.pe") : ""),
			CodigoSede = trabajador.CodigoSede,
			CodigoUnidad = trabajador.CodigoUnidad,
			CodigoPuesto = trabajador.CodigoPuesto,
			FechaIncorporacion = trabajador.FechaIngresoCorporacion,
			Sexo = trabajador.Sexo
		};
	}

	public List<TrabajadorDTOOut> TrabajadorVMListToTrabajadorDTOList(List<TrabajadorVM> trabajadores)
	{
		List<TrabajadorDTOOut> list = new List<TrabajadorDTOOut>();
		foreach (TrabajadorVM trabajadore in trabajadores)
		{
			list.Add(TrabajadorVMToTrabajadorDTO(trabajadore));
		}
		return list;
	}

	internal IEnumerable<ColaboradorRHDTOOut> ColaboradorRHVMListToColaboradorRHDTOList(List<ColaboradorRHVM> colaboradorRHVMs)
	{
		List<ColaboradorRHDTOOut> list = new List<ColaboradorRHDTOOut>();
		foreach (ColaboradorRHVM colaboradorRHVM in colaboradorRHVMs)
		{
			list.Add(ColaboradorRHVMToColaboradorRHDTO(colaboradorRHVM));
		}
		return list;
	}

	internal ColaboradorRHVM ColaboradorRHDTOToColaboradorRHVM(ColaboradorRHDTOCreate colaboradorRHDTO)
	{
		return ((IMapperBase)mapper).Map<ColaboradorRHVM>((object)colaboradorRHDTO);
	}

	internal ColaboradorRHVM ColaboradorRHDTOToColaboradorRHVM(ColaboradorRHDTOUpdate colaboradorRHDTO)
	{
		return ((IMapperBase)mapper).Map<ColaboradorRHVM>((object)colaboradorRHDTO);
	}

	internal ColaboradorRHDTOOut ColaboradorRHVMToColaboradorRHDTO(ColaboradorRHVM colaborador)
	{
		return new ColaboradorRHDTOOut
		{
			IdColaborador = colaborador.IdColaborador,
			DNI = colaborador.DNI,
			NombreApellidos = colaborador.NombreApellidos,
			Usuario = colaborador.Usuario,
			Sexo = colaborador.Sexo,
			FechaIncorporacion = colaborador.FechaIncorporacion,
			Unidad = colaborador.Unidad,
			Sede = colaborador.Sede,
			Puesto = colaborador.Puesto,
			Estado = colaborador.Estado,
			Region = RegionVMToRegionDTO(colaborador.Region)
		};
	}

	internal PeriodoVM PeriodoDTOToPeriodoVM(PeriodoDTOCreate periodo)
	{
		return ((IMapperBase)mapper).Map<PeriodoVM>((object)periodo);
	}

	internal PeriodoVM PeriodoDTOToPeriodoVM(PeriodoDTOUpdate periodo)
	{
		return ((IMapperBase)mapper).Map<PeriodoVM>((object)periodo);
	}

	internal List<PeriodoDTOOut> PeriodoVMListToPeriodoDTOList(List<PeriodoVM> periodos)
	{
		List<PeriodoDTOOut> list = new List<PeriodoDTOOut>();
		foreach (PeriodoVM periodo in periodos)
		{
			list.Add(PeriodoVMToPeriodoDTO(periodo));
		}
		return list;
	}

	internal PeriodoDTOOut PeriodoVMToPeriodoDTO(PeriodoVM periodo)
	{
		return new PeriodoDTOOut
		{
			IdPeriodo = periodo.IdPeriodo,
			FechaInicio = periodo.FechaInicio,
			FechaFin = periodo.FechaFin,
			FechaCorte = periodo.FechaCorte,
			FechaInicioCorte = periodo.FechaInicioCorte,
			UsuarioCrea = periodo.UsuarioCrea,
			NombreUsuarioCreacion = periodo.NombreUsuarioCreacion,
			TipoPeriodo = periodo.TipoPeriodo,
			DescripcionTipoPeriodo = periodo.DescripcionTipoPeriodo,
			EstadoPeriodo = periodo.EstadoPeriodo,
			DescripcionEstadoPeriodo = periodo.DescripcionEstadoPeriodo,
			Estado = periodo.Estado
		};
	}

	internal List<SedeDTO> SedeVMToSedeDTO(List<SedeVM> sedes)
	{
		return ((IMapperBase)mapper).Map<List<SedeDTO>>((object)sedes);
	}

	internal List<UnidadDTO> UnidadVMToUnidadDTO(List<UnidadVM> unidades)
	{
		return ((IMapperBase)mapper).Map<List<UnidadDTO>>((object)unidades);
	}

	internal List<PuestoDTO> PuestoVMToPuestoDTO(List<PuestoVM> puestos)
	{
		return ((IMapperBase)mapper).Map<List<PuestoDTO>>((object)puestos);
	}

	internal RegionDTO RegionVMToRegionDTO(RegionVM region)
	{
		return ((IMapperBase)mapper).Map<RegionDTO>((object)region);
	}

	internal List<RegionDTO> RegionVMListToRegionDTOList(List<RegionVM> regiones)
	{
		return ((IMapperBase)mapper).Map<List<RegionDTO>>((object)regiones);
	}

	internal ItemPedidoDTOOut ItemPedidoVMToItemPedidoDTOOut(ItemPedidoVM itemPedido)
	{
		return new ItemPedidoDTOOut
		{
			IdItemPedido = itemPedido.IdItemPedido,
			Uniforme = UniformeVMToUniformeDTO(itemPedido.Uniforme),
			Talla = ((IMapperBase)mapper).Map<TallaDTO>((object)itemPedido.Talla),
			Cantidad = itemPedido.Cantidad,
			Periodo = PeriodoVMToPeriodoDTO(itemPedido.Periodo),
			EstadoItem = itemPedido.EstadoItem,
			TipoItem = itemPedido.TipoItem
		};
	}

	public List<ItemPedidoDTOOut> ItemPedidoVMListToItemPedidoDTOList(List<ItemPedidoVM> itemsPedidos)
	{
		List<ItemPedidoDTOOut> list = new List<ItemPedidoDTOOut>();
		foreach (ItemPedidoVM itemsPedido in itemsPedidos)
		{
			list.Add(ItemPedidoVMToItemPedidoDTOOut(itemsPedido));
		}
		return list;
	}

	internal PedidoDTOOut PedidoVMToPedidoDTOOut(PedidoVM pedido)
	{
		return new PedidoDTOOut
		{
			IdPedido = pedido.IdPedido,
			Total = pedido.Total,
			Cuotas = pedido.Cuotas,
			TipoPedido = pedido.TipoPedido,
			EstadoPedido = pedido.EstadoPedido,
			UsuarioCrea = pedido.UsuarioCrea,
			ItemsPedidos = ItemPedidoVMListToItemPedidoDTOList(pedido.ItemsPedidos)
		};
	}

	internal List<PedidoDTOOut> PedidoVMListToPedidoDTOOutList(List<PedidoVM> pedidos)
	{
		List<PedidoDTOOut> list = new List<PedidoDTOOut>();
		foreach (PedidoVM pedido in pedidos)
		{
			list.Add(PedidoVMToPedidoDTOOut(pedido));
		}
		return list;
	}

	internal ItemPedidoVM ItemPedidoDTOToItemPedidoVM(ItemPedidoDTOCreate itemPedido)
	{
		return ((IMapperBase)mapper).Map<ItemPedidoVM>((object)itemPedido);
	}

	internal ItemPedidoVM ItemPedidoDTOToItemPedidoVM(ItemPedidoDTOUpdate itemPedido)
	{
		return ((IMapperBase)mapper).Map<ItemPedidoVM>((object)itemPedido);
	}

	internal PedidoVM PedidoDTOCreateToPedidoVM(PedidoDTOCreate pedido)
	{
		return ((IMapperBase)mapper).Map<PedidoVM>((object)pedido);
	}

	internal PedidoVM PedidoDTOUpdateToPedidoVM(PedidoDTOUpdate pedido)
	{
		return ((IMapperBase)mapper).Map<PedidoVM>((object)pedido);
	}

	internal EmailVM EmailDTOToEmailVM(EmailDTO pedido)
	{
		return ((IMapperBase)mapper).Map<EmailVM>((object)pedido);
	}

	internal KardexVM KardexDTOToKardexVM(KardexDTOCreate kardex)
	{
		return ((IMapperBase)mapper).Map<KardexVM>((object)kardex);
	}

	internal KardexVM KardexDTOToKardexVM(KardexDTOUpdate kardex)
	{
		return ((IMapperBase)mapper).Map<KardexVM>((object)kardex);
	}

	internal List<KardexDTOOut> KardexVMListToKardexDTOList(List<KardexVM> lista_kardex)
	{
		List<KardexDTOOut> list = new List<KardexDTOOut>();
		foreach (KardexVM item in lista_kardex)
		{
			list.Add(KardexVMToKardexDTO(item));
		}
		return list;
	}

	internal KardexDTOOut KardexVMToKardexDTO(KardexVM kardex)
	{
		return new KardexDTOOut
		{
			IdKardex = kardex.IdKardex,
			IdUniforme = kardex.IdUniforme,
			Uniforme = kardex.Uniforme,
			IdTalla = kardex.IdTalla,
			Talla = kardex.Talla,
			FechaRegistro = kardex.FechaRegistro,
			Concepto = kardex.Concepto,
			TipoRegistro = kardex.TipoRegistro,
			DescripcionTipoRegistro = kardex.DescripcionTipoRegistro,
			Valor = kardex.Valor,
			NombreUsuarioCreacion = kardex.NombreUsuarioCreacion,
			FechaCrea = kardex.FechaCrea,
			CantidadInicial = (kardex.CantidadInicial.HasValue ? kardex.CantidadInicial.Value : 0),
			CantidadFinal = (kardex.CantidadFinal.HasValue ? kardex.CantidadFinal.Value : 0)
		};
	}

	internal BannerVM BannerDTOToBannerVM(BannerDTOCreate bannerDTO)
	{
		BannerVM bannerVM = new BannerVM();
		foreach (string gUIDImagene in bannerDTO.GUIDImagenes)
		{
			ImagenBannerVM imagenBannerVM = new ImagenBannerVM();
			imagenBannerVM.GUIDImagen = gUIDImagene;
			imagenBannerVM.UsuarioCrea = bannerDTO.UsuarioCrea;
			bannerVM.Imagenes.Add(imagenBannerVM);
		}
		return bannerVM;
	}
}

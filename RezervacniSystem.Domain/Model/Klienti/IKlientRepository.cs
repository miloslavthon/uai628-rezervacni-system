﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Klienti
{
	public interface IKlientRepository
	{
		Klient Vrat(int id);
		Klient VratKlientaDleUzivatelskehoJmena(String uzivatelskeJmeno);
		int VratIdKlientaDleUzivatelskehoJmena(String uzivatelskeJmeno);
		void Uloz(Klient klient);
	}
}

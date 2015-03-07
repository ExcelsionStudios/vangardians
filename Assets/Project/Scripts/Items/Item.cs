﻿using UnityEngine;
using System.Collections;
using Excelsion.Towers;
using Excelsion.Towers.Projectiles;

//Stephan Ennen - 3/7/2015

namespace Excelsion.Inventory
{
	//RIGHT NOW THIS CLASS IS ONLY A PLACEHOLDER FOR OTHER SCRIPTS.
	public abstract class Item : System.Object
	{
		public abstract int Priority{ get; } //Lower values will be overwritten by higher values.
		public abstract int MutexBits{ get; } //Runs a bitwise AND operation to see if we can use this item alongside another item.
		public abstract TowerStats Stats{ get; }

		//Return display name
		public abstract string Name{ get; }
		//Return display icon location
		public abstract string Icon{ get; }
		//TODO - Add model / effect changes

		//Called in the tower's update loop. You could do things like auras or independent projectiles here.
		public virtual void OnTowerUpdate()
		{
			return;
		}
		//Called before a projectile is created. Use this to create multiple projectiles..?
		public virtual void OnPreProjectileCreated()
		{
			return;
		}
		//Called after our projectile is created. Use this to pass delegates or other info to the projectile.
		public virtual void OnProjectileCreated( ProjectileBase projectile )
		{
			return;
		}
		//Called in the projectile's update loop. Pass as a delegate in OnProjectileCreated.
		public virtual void OnProjectileUpdate()
		{
			return;
		}
		//This is given to the projectile as a delegate.
		public virtual void OnProjectileImpact() //TODO - pass array of enemies hit here.
		{
			return;
		}














	}
}
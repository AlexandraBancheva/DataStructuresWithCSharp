namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Inventory : IHolder
    {
        private List<IWeapon> weapons = new List<IWeapon>();

        public int Capacity => this.weapons.Count;

        public void Add(IWeapon weapon)
        {
            this.weapons.Add(weapon);
        }

        public void Clear()
        {
            this.weapons.Clear();
        }

        public bool Contains(IWeapon weapon)
        {
            var contain = this.weapons.Contains(weapon);
            if (contain)
            {
                return true;
            }
            return false;
        }

        public void EmptyArsenal(Category category)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                var currentWeapon = this.weapons[i];
                if (currentWeapon.Category == category)
                {
                    currentWeapon.Ammunition = 0;
                }
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            if (!this.weapons.Contains(weapon))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (weapon.Ammunition < ammunition)
            {
                return false;
            }
            weapon.Ammunition -= ammunition;

            return true;
        }

        public IWeapon GetById(int id)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                var currentWeapon = this.weapons[i];
                if (currentWeapon.Id == id)
                {
                    return currentWeapon;
                }
            }

            return null;
        }

        public IEnumerator GetEnumerator()
        {
           return this.weapons.GetEnumerator();
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            if (!this.weapons.Contains(weapon))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            weapon.Ammunition += ammunition;
            if (weapon.Ammunition > weapon.MaxCapacity)
            {
                weapon.Ammunition = weapon.MaxCapacity;
            }

            return weapon.Ammunition;
        }

        public IWeapon RemoveById(int id)
        {
            var weapon = this.GetById(id);
            if (!this.weapons.Contains(weapon))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
            this.weapons.Remove(weapon);
            return weapon;
        }

        public int RemoveHeavy()
        {
            return this.weapons.RemoveAll(w => w.Category == Category.Heavy);
        }

        public List<IWeapon> RetrieveAll()
        {
            return this.Capacity == 0 ? new List<IWeapon>() : new List<IWeapon>(this.weapons);
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            return this.Capacity == 0 ? new List<IWeapon>() : this.weapons.Where(w => w.Category >= lower && w.Category <= upper).ToList();
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            if (!this.weapons.Contains(firstWeapon) || !this.weapons.Contains(secondWeapon))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (firstWeapon.Category == secondWeapon.Category)
            {
                var firstIndex = this.weapons.IndexOf(firstWeapon);
                var secondIndex = this.weapons.IndexOf(secondWeapon);

                var tempIndex = this.weapons[firstIndex];
                this.weapons[firstIndex] = this.weapons[secondIndex];
                this.weapons[secondIndex] = tempIndex;
            }
        }
    }
}

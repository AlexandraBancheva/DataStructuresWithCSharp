namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _02.LegionSystem.Interfaces;

    public class Legion : IArmy
    {
        private HashSet<IEnemy> enemies = new HashSet<IEnemy>();

        public int Size => this.enemies.Count;

        public bool Contains(IEnemy enemy)
        {
            return this.enemies.Contains(enemy);
        }

        public void Create(IEnemy enemy)
        {
                this.enemies.Add(enemy);
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            var res = this.enemies.
                FirstOrDefault(e => e.AttackSpeed == speed);
            return res ?? null;
        }

        public List<IEnemy> GetFaster(int speed)
        {
            return this.enemies.Where(e => e.AttackSpeed > speed).ToList();
        }

        public IEnemy GetFastest()
        {
            CheckTheSizeOfLegion();
            return this.enemies.LastOrDefault();
        }

        public IEnemy[] GetOrderedByHealth()
        {
            return this.enemies.OrderByDescending(h => h.Health).ToArray();
        }

        public List<IEnemy> GetSlower(int speed)
        {
            return this.enemies.Where(e => e.AttackSpeed < speed).ToList();
        }

        public IEnemy GetSlowest()
        {
            CheckTheSizeOfLegion();
            return this.enemies.FirstOrDefault();
        }

        public void ShootFastest()
        {
            CheckTheSizeOfLegion();
            this.enemies.Remove(this.enemies.LastOrDefault());
        }

        public void ShootSlowest()
        {
            CheckTheSizeOfLegion();
            this.enemies.Remove(this.enemies.FirstOrDefault());
        }

        private bool CheckTheSizeOfLegion()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }
            return true;
        }
    }
}

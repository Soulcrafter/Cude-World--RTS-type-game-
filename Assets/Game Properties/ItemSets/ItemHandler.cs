using Game_Properties.ItemSets.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Properties.ItemSets {

    public abstract class ItemHandler {

        private INItem Class;

        public ItemHandler(INItem Class) {
            this.Class = Class;
        }



        public virtual void Tick() { }
        
        public virtual void Render() { }


    }
}

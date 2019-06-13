using Game_Properties.ItemSets.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game_Properties.ItemSets.IPhysical {

    public class Dirt : Block {

        public override int Value {
            get {
                return 0x0200;
            }
            set {
                this.Value = value;
            }
        }

        protected override string Name {
            get {
                return this.GetType().Name;
            }
        }

        public override ItemHandler Handler {
            get {
                return new DirtBlockHandler(this);
            }
        }

        public override Color32 Color {
            get {
                return new Color32(160,108,108, 0);
                ;
            }
        }
    }

    public class DirtBlockHandler : ItemHandler {

        public DirtBlockHandler(INItem Class) : base(Class) {

        }

    }

}

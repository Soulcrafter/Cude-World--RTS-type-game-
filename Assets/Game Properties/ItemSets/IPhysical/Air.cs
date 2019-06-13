using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game_Properties.ItemSets;
using Game_Properties.ItemSets.IPhysical;
using Game_Properties.ItemSets.interfaces;

namespace Game_Properties.ItemSets.IPhysical {


    public class Air : Block {

        protected override string Name {
            get {
                return this.GetType().Name;
            }
        }

        public override ItemHandler Handler {
            get {
                return new AirBlockHandler(this);
            }
        }

        public override int Value {
            get {
                return 0x00;
            }
            set {
                this.Value = value;
            }
        }

        public override Color32 Color {
            get {
                return new Color();
            }
        }
    }



    public class AirBlockHandler : ItemHandler {

        public AirBlockHandler(INItem Class) : base(Class) {

        }

    }
}

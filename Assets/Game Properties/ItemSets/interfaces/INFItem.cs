using System;
using System.Collections.Generic;
using Game_Properties.ItemSets;
using System.Linq;
using System.Text;

namespace Game_Properties.ItemSets.interfaces {
    
    public interface INItem {

        ItemSetType GetItemSetType();

        bool HasValue();

        bool InitializeID(int id);

    }

}

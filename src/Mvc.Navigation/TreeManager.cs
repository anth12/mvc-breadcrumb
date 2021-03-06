﻿using System.Collections.Generic;
using System.Linq;

namespace Mvc.Navigation
{
    internal class TreeManager : ITreeManager
	{
        private readonly IEnumerable<ITreeBuilder> _providers;
        private readonly TreeIndex _index;

        public TreeManager(IEnumerable<ITreeBuilder> providers, TreeIndex index)
        {
            _providers = providers;
            _index = index;
        }

        public void Build()
        {
            var rootElements = _providers
                .SelectMany(p => p.GetElements() ?? new TreeElement[0])
                .Where(e=> e != null);

            _index.Build(rootElements);
        }

    }
}

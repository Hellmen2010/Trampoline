using System.Collections.Generic;
using Trampoline.CodeBase.Core.Element;
using Trampoline.CodeBase.Data.Enums;
using Trampoline.CodeBase.Services.Factories.UIFactory;

namespace Trampoline.CodeBase.Core.UI.Statistics
{
    public class LastElements
    {
        private readonly LastElementsTopPanelView _topPanelView;
        private readonly IUIFactory _uiFactory;
        private readonly Dictionary<ElementType, Element.Element> _elementsByType;
        private bool _isTopPanelFull;
        private ElementUIView[] _elements;

        public LastElements(LastElementsTopPanelView topPanelView, Dictionary<ElementType, Element.Element> elementsByType, IUIFactory uiFactory, 
            int topPanelSize)
        {
            _topPanelView = topPanelView;
            _elementsByType = elementsByType;
            _uiFactory = uiFactory;
            IniElements(topPanelSize);
        }

        public void AddTopPanelElement(ElementType elementType)
        {
            if (!_isTopPanelFull)
            {
                if (TryAddElement(elementType)) return;
                _isTopPanelFull = true;
            }
            ChangeAllElements(elementType);
        }

        private bool TryAddElement(ElementType elementType)
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                if (_elements[i] != null) continue;
                _elements[i] = _uiFactory.CreateTopPanelElement(_elementsByType[elementType], _topPanelView.ElementsRoot);
                return true;
            }
            return false;
        }

        private void ChangeAllElements(ElementType elementType)
        {
            for (int i = _elements.Length - 1; i > 0; i--)
                _elements[i].SetElement(_elements[i - 1]);
            _elements[0].SetElement(_elementsByType[elementType]);
        }

        private void IniElements(int amount) => _elements = new ElementUIView[amount];
    }
}
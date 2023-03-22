using System.Collections.Generic;
using Trampoline.CodeBase.Data.Enums;
using Trampoline.CodeBase.Data.StaticData.Grid;
using Trampoline.CodeBase.Services.EntityContainer;
using Trampoline.CodeBase.Services.Factories.UIFactory;
using Trampoline.CodeBase.Services.StaticData;
using UnityEngine;

namespace Trampoline.CodeBase.Core.UI.Statistics
{
    public class Statistics
    {
        private readonly StatisticsPanelView _panelView;
        private readonly GridConfig _config;
        private readonly IUIFactory _uiFactory;
        private readonly IEntityContainer _entityContainer;
        private StatisticElementView[] _statisticsElements;
        private readonly Dictionary<ElementType,Element.Element> _elementsByType;

        public Statistics(StatisticsPanelView panelView, IUIFactory uiFactory, IEntityContainer entityContainer, IStaticData staticData)
        {
            _elementsByType = staticData.ElementsByType;
            _panelView = panelView;
            _config = staticData.GridConfig;
            _uiFactory = uiFactory;
            _entityContainer = entityContainer;
        }

        public void SubscribeBackButton()
        {
            _panelView.OnShow += EnableBackButton;
            _panelView.OnHide += DisableBackButton;
        }
        
        public void UnSubscribeBackButton()
        {
            _panelView.OnShow -= EnableBackButton;
            _panelView.OnHide -= DisableBackButton;
        }

        public void HideView() => _panelView.Hide();

        public void ShowView() => _panelView.Show();

        public void CreateStatistics()
        {
            _statisticsElements = new StatisticElementView[_config.Columns * _config.Rows];

            float fieldHeight = CalculateFieldHeight();

            float cellSize = (fieldHeight - _config.TopPadding - _config.BottomPadding -
                              _config.YSpacing * (_config.Rows - 1)) / _config.Rows;
            float fieldWidth = cellSize * _config.Columns + _config.LeftPadding + _config.RightPadding +
                               _config.XSpacing * (_config.Columns - 1);
            ChangeBackgroundSize(fieldWidth, fieldHeight);
            FillStatisticsGrid(cellSize);
        }

        private float CalculateFieldHeight()
        {
            return _panelView.Field.anchorMax.y - _panelView.Field.anchorMin.y < 0.01f
                ? _panelView.Field.sizeDelta.y
                : (_panelView.Field.anchorMax.y - _panelView.Field.anchorMin.y) * Screen.height;
        }

        private void ChangeBackgroundSize(float fieldWidth, float fieldHeight) => 
            _panelView.BackgroundRect.sizeDelta = new Vector2(fieldWidth, fieldHeight);

        private void FillStatisticsGrid(float cellSize)
        {
            Vector2 firstElementRect = new Vector2(_config.LeftPadding, _config.TopPadding);
            for (int i = 0; i < _config.Rows; i++)
            {
                for (int j = 0; j < _config.Columns; j++)
                {
                    Vector2 pos = CalculateElementViewPos(cellSize, firstElementRect, j, i);
                    StatisticElementView elementView = _uiFactory.CreateStatisticsElementView(_panelView.Field);
                    elementView.SetupRect(cellSize, pos);
                    _statisticsElements[i * _config.Columns + j] = elementView;
                }
            }
        }

        private Vector2 CalculateElementViewPos(float cellSize, Vector2 firstElementRect, int j, int i) =>
            new (firstElementRect.x + j * cellSize + _config.XSpacing * j,
                -firstElementRect.y - i * cellSize - _config.YSpacing * i);

        private void EnableBackButton() => _entityContainer.GetEntity<BackButton>().Show();
        
        private void DisableBackButton() => _entityContainer.GetEntity<BackButton>().Hide();

        public void FillPanel(List<ElementType> progressStatisticsElements)
        {
            int number = 0;
            for (int i = progressStatisticsElements.Count - 1; i > 0; i--)
            {
                _statisticsElements[number].SetElement(_elementsByType[progressStatisticsElements[i]]);
                _statisticsElements[number].Show();
                number++;
            }
        }
    }
}
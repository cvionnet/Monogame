using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace Nucleus
{
    public class Nucleus_Tiles_TMX
    {
        //private TmxMap _map;
        private static TiledMap _tileMap;
        private static TiledMapRenderer _tileMapRenderer;

        private static int _tileWidth;
        private static int _tileHeight;
        private static int _mapWidth;
        private static int _mapHeight;
        private static int _tilesetLines;
        private static int _tilesetCols;
        public static int TileWidth { get => _tileWidth; }
        public static int TileHeight { get => _tileHeight; }
        public static int MapWidth { get => _mapWidth; }
        public static int MapHeight { get => _mapHeight; }
        public static int TilesetLines { get => _tilesetLines; }
        public static int TilesetCols { get => _tilesetCols; }

        //*---------------------------------------------------------------------------------
        //* METHODS
        //*---------------------------------------------------------------------------------

        public static void Initialize(TiledMap pTiledMap, GraphicsDevice pGraphicsDevice)
        {
            _tileMap = pTiledMap;
            _tileMapRenderer = new TiledMapRenderer(pGraphicsDevice, _tileMap);

            _tileWidth = _tileMap.TileWidth;
            _tileHeight = _tileMap.TileHeight;
            _mapWidth = _tileMap.Width;
            _mapHeight = _tileMap.Height;
            _tilesetCols = _tileMap.Tilesets[0].ActualWidth / _tileWidth;
            _tilesetLines = _tileMap.Tilesets[0].ActualHeight / _tileHeight;
        }

        public static void Update(GameTime gameTime)
        {
            _tileMapRenderer.Update(gameTime);
        }

        /// <summary>
        /// Draw all or 1 layer of the tilemap
        /// </summary>
        /// <param name="pLayerID">(optional) ID of the layer to draw</param>
        public static void Draw(int? pLayerID = null)
        {
            if (pLayerID == null)
                _tileMapRenderer.Draw();
            else
                _tileMapRenderer.Draw((int)pLayerID);
        }
    }
}
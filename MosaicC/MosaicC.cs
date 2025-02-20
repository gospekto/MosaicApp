using System;

namespace MosaicCLibrary
{
    public class MosaicC
    {
        public static unsafe void ApplyMosaic(byte[] sourceBuffer, byte[] resultBuffer, int height, int width, int stride, int start, int end, int tileSize)
        {
            {

                int startRow = start / (stride);
                int endRow = end / (stride);

                for (int y = startRow; y < endRow; y += tileSize)
                {
                    for (int x = 0; x < width; x += tileSize)
                    {

                        int currentTileWidth = Math.Min(tileSize, width - x);
                        int currentTileHeight = Math.Min(tileSize, height - y);

                        long totalR = 0, totalG = 0, totalB = 0;
                        int pixelCount = 0;

                        for (int ty = 0; ty < currentTileHeight; ty++)
                        {
                            if ((y + ty) >= endRow) break;

                            for (int tx = 0; tx < currentTileWidth; tx++)
                            {
                                int pixelOffset = ((y + ty) * stride) + ((x + tx) * 4);

                                byte b = sourceBuffer[pixelOffset];
                                byte g = sourceBuffer[pixelOffset + 1];
                                byte r = sourceBuffer[pixelOffset + 2];

                                totalB += b;
                                totalG += g;
                                totalR += r;
                                pixelCount++;
                            }
                        }

                        if (pixelCount > 0)
                        {
                            byte avgB = (byte)(totalB / pixelCount);
                            byte avgG = (byte)(totalG / pixelCount);
                            byte avgR = (byte)(totalR / pixelCount);

                            for (int ty = 0; ty < currentTileHeight; ty++)
                            {
                                if ((y + ty) >= endRow) break;

                                for (int tx = 0; tx < currentTileWidth; tx++)
                                {
                                    int pixelOffset = ((y + ty) * stride) + ((x + tx) * 4);

                                    resultBuffer[pixelOffset] = avgB;
                                    resultBuffer[pixelOffset + 1] = avgG;
                                    resultBuffer[pixelOffset + 2] = avgR;

                                    resultBuffer[pixelOffset + 3] = sourceBuffer[pixelOffset + 3];
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

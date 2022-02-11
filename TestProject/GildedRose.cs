using System.Collections.Generic;

namespace TestProject
{
    public class GildedRose
    {
        IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
               if (Items[i].Name == "Aged Brie")
                {
                    AgedBrieProduct agedBrie = new AgedBrieProduct();
                    Items[i] = agedBrie.UpdateQuality(Items[i]); ;
                    break;
                }

                if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    BackstageTicketProduct backstagePass = new BackstageTicketProduct();
                    Items[i] = backstagePass.UpdateQuality(Items[i]); 
                    break;

                }

                if (Items[i].Name == "Sulfuras, Hand of Ragnaros")
                {
                    SulfurasProduct sulfuras = new SulfurasProduct();
                    Items[i] = sulfuras.UpdateQuality(Items[i]);
                    break;
                }

                // normal
                NormalProduct product = new NormalProduct();
                Items[i] = product.UpdateQuality(Items[i]);
            }
        }

        public void UpdateQualityV2()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != "Aged Brie")
                    {
                        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }

        public override string ToString()
        {
            return this.Name + ", " + this.SellIn + ", " + this.Quality;
        }
    }



    //Bastante simple, ¿no? Bueno, ahora es donde se pone interesante:

    //    - caso decremento x1
    //    - Una vez que ha pasado la fecha recomendada de venta, la calidad se degrada al doble de velocidad
    //    - La calidad de un artículo nunca es negativa
    //    - El "Queso Brie envejecido" (Aged brie) incrementa su calidad a medida que se pone viejo
    //        - Su calidad aumenta en 1 unidad cada día
    //        - luego de la fecha de venta su calidad aumenta 2 unidades por día
    //    - La calidad de un artículo nunca es mayor a 50
    //    - El artículo "Sulfuras" (Sulfuras), siendo un artículo legendario, no modifica su fecha de venta ni se degrada en calidad
    //        - Para aclarar: un artículo nunca puede tener una calidad superior a 50, sin embargo las Sulfuras siendo un artículo legendario posee una calidad inmutable de 80.


    public interface IProduct{
        public Item UpdateQuality(Item item);
    }

    public class NormalProduct : IProduct
    {
        public Item UpdateQuality(Item item)
        {
            int sellInUpdateValue = -1;
            int qualityUpdateValue = -1;

            if (item.SellIn <= 0 && item.Quality -2 > 0)
            {
                qualityUpdateValue = -2;
            }

            if (item.Quality == 0)
            {
                qualityUpdateValue = 0;
            }

            var updatedItem = new Item { Name = item.Name, SellIn = item.SellIn + sellInUpdateValue, Quality = item.Quality + qualityUpdateValue };
            return updatedItem;
        }
    }

    public class AgedBrieProduct : IProduct
    {
        public Item UpdateQuality(Item item)
        {
            int sellInUpdateValue = -1;
            int qualityUpdateValue = 1;
            int qualityValue;

            if (item.SellIn <= 0)
            {
                qualityUpdateValue = 2;
            }

            if (item.Quality > 50)
            {
                qualityUpdateValue = 0;
            }

            qualityValue = item.Quality + qualityUpdateValue;

            if (item.Quality < 50 && qualityValue > 50)
            {
                qualityValue = 50;
            }

            var updatedItem = new Item { Name = item.Name, SellIn = item.SellIn + sellInUpdateValue, Quality = qualityValue };
            return updatedItem;
        }
    }

    public class SulfurasProduct : IProduct
    {
        public Item UpdateQuality(Item item)
        {
            return item;
        }
    }

    //    - Una "Entrada al Backstage", como el queso brie, incrementa su calidad a medida que la fecha de venta se aproxima
    //        - si faltan 10 días o menos para el concierto, la calidad se incrementa en 2 unidades
    //        - si faltan 5 días o menos, la calidad se incrementa en 3 unidades
    //        - luego de la fecha de venta la calidad cae a 0

    public class BackstageTicketProduct : IProduct
    {
        public Item UpdateQuality(Item item)
        {
            int sellInUpdateValue = -1;
            int qualityUpdateValue = 1;

            if (item.SellIn > 5 && item.SellIn <= 10)
            {
                qualityUpdateValue = 2;
            }

            if (item.SellIn >= 1 && item.SellIn <= 5)
            {
                qualityUpdateValue = 3;
            }

            if (item.SellIn <= 0)
            {
                qualityUpdateValue = -item.Quality;
            }

            var updatedItem = new Item { Name = item.Name, SellIn = item.SellIn + sellInUpdateValue, Quality = item.Quality + qualityUpdateValue };
            return updatedItem;
        }
    }


}
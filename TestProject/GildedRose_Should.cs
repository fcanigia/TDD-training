using System.Collections.Generic;
using Xunit;

namespace TestProject
{
    public class GildedRose_Should
    {

        /*
         * Descripción preliminar
            Pero primero, vamos a introducir el sistema:

            - Todos los artículos (Item) tienen una propiedad sellIn que denota el número de días que tenemos para venderlo
            - Todos los artículos tienen una propiedad Quality que denota cúan valioso es el artículo
            - Al final de cada día, nuestro sistema decrementa ambos valores para cada artículo mediante el método updateQuality

            Bastante simple, ¿no? Bueno, ahora es donde se pone interesante:
            
            - caso decremento x1
            - Una vez que ha pasado la fecha recomendada de venta, la calidad se degrada al doble de velocidad
            - La calidad de un artículo nunca es negativa
            - El "Queso Brie envejecido" (Aged brie) incrementa su calidad a medida que se pone viejo
                - Su calidad aumenta en 1 unidad cada día
                - luego de la fecha de venta su calidad aumenta 2 unidades por día
            - La calidad de un artículo nunca es mayor a 50
            - El artículo "Sulfuras" (Sulfuras), siendo un artículo legendario, no modifica su fecha de venta ni se degrada en calidad
                - Para aclarar: un artículo nunca puede tener una calidad superior a 50, sin embargo las Sulfuras siendo un artículo legendario posee una calidad inmutable de 80.
            - Una "Entrada al Backstage", como el queso brie, incrementa su calidad a medida que la fecha de venta se aproxima
                - si faltan 10 días o menos para el concierto, la calidad se incrementa en 2 unidades
                - si faltan 5 días o menos, la calidad se incrementa en 3 unidades
                - luego de la fecha de venta la calidad cae a 0
         */

        [Fact]
        public void name_not_changed()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal("foo", Items[0].Name);
        }

        [Fact]
        public void normal_product_sell_in_decrease_by_one()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "normal product", SellIn = 5, Quality = 5 }
                                                , new Item { Name = "Aged Brie", SellIn = 5, Quality = 5 } 
                                                , new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 40 }};
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(4, Items[0].SellIn);
        }

        [Fact]
        public void normal_product_quality_decrease_by_one()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "normal product", SellIn = 5, Quality = 5 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(4, Items[0].Quality);
        }

        [Fact]
        public void normal_product_quality_decrease_by_double_where_expired()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "normal product", SellIn = 0, Quality = 10 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(8, Items[0].Quality);
        }

        [Fact]
        public void normal_product_quality_never_negative()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "normal product", SellIn = 0, Quality = 1 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void aged_brie_quality_should_increase_by_one()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 5, Quality = 5 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(6, Items[0].Quality);
        }

        [Fact]
        public void aged_brie_quality_should_increase_by_two_when_expired()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 5 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(7, Items[0].Quality);
        }

        [Fact]
        public void aged_brie_quality_can_not_be_greater_than_fifty()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 49 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(50, Items[0].Quality);
        }

        [Fact]
        public void aged_brie_quality_doesnt_change_if_greater_than_fifty()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 55 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(55, Items[0].Quality);
        }

        [Fact]
        public void sulfuras_static_sell_in()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 40 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(10, Items[0].SellIn);
        }

        [Fact]
        public void sulfuras_static_quality()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 81 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(81, Items[0].Quality);
        }

        [Fact]
        public void backstage_increase_quality_by_one_when_sell_in_greater_than_ten()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 40 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(41, Items[0].Quality);
        }

        [Fact]
        public void backstage_increase_quality_by_two_when_sell_in_between_six_and_ten()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 40 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(42, Items[0].Quality);
        }

        [Fact]
        public void backstage_increase_quality_by_three_when_sell_in_between_one_and_five()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 4, Quality = 40 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(43, Items[0].Quality);
        }

        [Fact]
        public void backstage_quality_to_zero_when_expired()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = -1, Quality = 40 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void normal_product_with_sulfuras_name_sell_in_decrease_by_one()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "normal Sulfuras product", SellIn = 10, Quality = 40 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(9, Items[0].SellIn);
        }

        [Fact]
        public void normal_product_with_sulfuras_name_quality_decrease_by_one()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "normal Sulfuras product", SellIn = 10, Quality = 40 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(39, Items[0].Quality);
        }

        [Fact]
        public void normal_product_with_minus_ten_sell_in_decrease_by_one()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "normal Sulfuras product", SellIn = -10, Quality = 40 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(-11, Items[0].SellIn);
        }

    }
}



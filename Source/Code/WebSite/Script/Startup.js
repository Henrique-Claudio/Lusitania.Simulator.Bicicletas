jQuery(document).ready(function()
{
    jQuery('#carousel').Carousel(
		{
			itemWidth: 200,
			itemHeight: 50,
			itemMinWidth: 50,
			items: 'a',
			reflections: .5,
			rotationSpeed: 1.8
		}
	);
});
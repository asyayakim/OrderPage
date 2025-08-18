namespace ECommerceApp.Domain;

public class ProductCategorizer
{

    private static readonly string[] BeverageKeywords =
    {
        "cola", "juice", "soda", "water", "tea", "coffee", "drink", "beverage",
        "lemonade", "iced tea", "energy drink", "sports drink", "smoothie", "brew",
        "espresso", "cappuccino", "latte", "infusion", "mineral water", "tonic"
    };

    private static readonly string[] DairyKeywords =
    {
        "milk", "cheese", "yogurt", "butter", "cream", "lactose", "curd", "whey",
        "ice cream", "sour cream", "cottage cheese", "cream cheese", "provolone",
        "mozzarella", "parmesan", "ricotta", "gouda", "brie", "feta", "buttermilk",
        "half-and-half", "whipping cream", "kefir"
    };

    private static readonly string[] BakeryKeywords =
    {
        "bread", "loaf", "baguette", "bun", "roll", "croissant", "pastry", "cake",
        "cookie", "muffin", "doughnut", "donut", "pie", "tart", "breadstick", "bagel",
        "scone", "biscuit", "ciabatta", "pita", "pretzel", "cupcake", "brownie",
        "eclair", "cannoli", "strudel", "waffle", "pancake", "crepe"
    };

    private static readonly string[] MeatPoultryKeywords =
    {
        "chicken", "beef", "pork", "lamb", "turkey", "duck", "steak", "mince",
        "sausage", "bacon", "ham", "veal", "venison", "bison", "ribs", "chop",
        "cutlet", "drumstick", "breast", "thigh", "wing", "ground beef", "roast",
        "salami", "pepperoni", "prosciutto", "jerky"
    };

    private static readonly string[] SeafoodKeywords =
    {
        "fish", "salmon", "tuna", "shrimp", "prawn", "crab", "lobster", "mussel",
        "oyster", "cod", "sardine", "trout", "tilapia", "bass", "halibut", "scallop",
        "squid", "octopus", "calamari", "clam", "caviar", "roe", "anchovy", "haddock",
        "mackerel", "snapper", "swordfish", "catfish"
    };

    private static readonly string[] FruitKeywords =
    {
        "apple", "banana", "orange", "pear", "peach", "plum", "grape", "kiwi",
        "mango", "strawberry", "blueberry", "raspberry", "lemon", "lime", "cherry",
        "pineapple", "watermelon", "cantaloupe", "melon", "coconut", "avocado",
        "papaya", "pomegranate", "fig", "apricot", "nectarine", "blackberry",
        "cranberry", "grapefruit", "tangerine", "mandarin", "guava", "lychee"
    };

    private static readonly string[] VegetableKeywords =
    {
        "potato", "tomato", "onion", "carrot", "lettuce", "cucumber", "pepper",
        "broccoli", "cauliflower", "spinach", "cabbage", "garlic", "pumpkin",
        "squash", "zucchini", "eggplant", "celery", "asparagus", "bean", "pea",
        "corn", "radish", "turnip", "artichoke", "sweet potato", "yam", "kale",
        "arugula", "beet", "brussel sprout", "scallion", "shallot", "leek", "okra",
        "rhubarb", "chard", "collard", "parsnip", "rutabaga", "jicama"
    };

    private static readonly string[] FrozenFoodKeywords =
    {
        "frozen", "freezer meal", "tv dinner", "popsicle", "ice pop", "frozen pizza",
        "frozen lasagna", "frozen burrito", "frozen waffle", "frozen dumpling",
        "french fry", "hash brown", "ice cube", "freezer burn", "frozen dessert"
    };

    private static readonly string[] SnackKeywords =
    {
        "chips", "crisps", "popcorn", "pretzel", "nuts", "trail mix", "snack bar",
        "chocolate", "candy", "sweets", "cracker", "potato chips", "nachos", "tortilla",
        "dip", "salsa", "guacamole", "energy bar", "granola bar", "fruit snack",
        "gummy", "jerky", "pudding", "jello", "cheese puff", "pork rind", "sunflower seed",
        "pistachio", "cashew", "almond", "peanut", "walnut", "pecan", "raisin", "prune",
        "fruit leather", "toffee", "caramel", "licorice"
    };

    private static readonly string[] PantryKeywords =
    {
        "rice", "pasta", "noodles", "flour", "sugar", "salt", "pepper", "oil", "vinegar",
        "sauce", "spice", "seasoning", "cereal", "oats", "beans", "lentils", "canned",
        "tomato sauce", "soy sauce", "pasta sauce", "broth", "stock", "bouillon",
        "baking powder", "baking soda", "yeast", "honey", "syrup", "jam", "jelly",
        "peanut butter", "canned fruit", "canned vegetable", "soup", "canned soup",
        "ketchup", "mustard", "mayonnaise", "relish", "pickle", "olive oil", "vegetable oil",
        "coconut oil", "sesame oil", "hot sauce", "worcestershire", "breadcrumbs", "stuffing",
        "gravy", "salsa", "pesto", "hummus", "tahini", "quinoa", "couscous", "bulgur",
        "canned fish", "canned meat", "evaporated milk", "condensed milk", "powdered milk",
        "coffee bean", "tea bag", "cocoa", "powdered drink"
    };

    private static readonly string[] HouseholdKeywords =
    {
        "soap", "detergent", "cleaner", "sponge", "brush", "mop", "bucket", "tissue",
        "paper towel", "toilet paper", "napkin", "garbage bag", "trash bag", "dish soap",
        "laundry detergent", "all-purpose cleaner", "bleach", "disinfectant", "window cleaner",
        "floor cleaner", "bathroom cleaner", "glass cleaner", "paper napkin", "facial tissue",
        "aluminum foil", "plastic wrap", "sandwich bag", "freezer bag", "storage container",
        "air freshener", "dishwasher detergent", "dishwasher tablet", "fabric softener",
        "dryer sheet", "paper plate", "plastic cup", "trash can", "broom", "dustpan",
        "glove", "spray bottle", "scouring pad", "dish cloth", "hand towel", "bath towel",
        "shower curtain", "toilet brush", "plunger", "light bulb", "battery"
    };

    private static readonly string[] PersonalCareKeywords =
    {
        "shampoo", "conditioner", "body wash", "toothpaste", "deodorant", "lotion", "cream",
        "razor", "shaving", "brush", "comb", "sanitary", "cotton swab", "cotton ball",
        "makeup", "cosmetic", "lip balm", "hand soap", "hand sanitizer", "facial cleanser",
        "mouthwash", "dental floss", "toothbrush", "shaving cream", "shaving gel", "after shave",
        "body lotion", "sunscreen", "suntan lotion", "perfume", "cologne", "hair gel",
        "hair spray", "hair conditioner", "moisturizer", "serum", "toner", "eye cream",
        "face mask", "foundation", "mascara", "lipstick", "nail polish", "tampon", "pad",
        "diaper", "baby wipe", "baby shampoo", "baby lotion", "contact solution", "saline",
        "vitamin", "supplement", "first aid", "bandage"
    };

    private static readonly string[] PetCareKeywords =
    {
        "pet", "dog", "cat", "food", "treat", "toy", "litter", "leash", "collar", "bed",
        "dog food", "cat food", "pet food", "bird seed", "fish food", "hamster food",
        "dog treat", "cat treat", "dog toy", "cat toy", "cat litter", "dog bed", "cat bed",
        "pet shampoo", "flea treatment", "tank cleaner", "aquarium", "bird cage", "pet carrier",
        "kibble", "rawhide", "scratching post", "crate", "harness", "poop bag", "bowl",
        "feeder", "waterer", "fish tank", "cage liner", "bird toy", "small animal bedding",
        "flea collar", "wormer", "vaccine", "grooming tool", "nail clipper"
    };

    public string GetCategory(KassalProduct product)
    {
        string name = product.Name?.ToLower() ?? "";
        string description = product.Description?.ToLower() ?? "";
        if (name.Contains("chocolate milk")) return "Dairy";
        if (name.Contains("veggie burger")) return "Vegetables";
        if (name.Contains("fruit juice")) return "Beverages";
        if (name.Contains("frozen pizza")) return "Frozen Foods";
        if (ContainsAny(name, description, BeverageKeywords))
            return "Beverages";

        if (ContainsAny(name, description, DairyKeywords))
            return "Dairy";

        if (ContainsAny(name, description, BakeryKeywords))
            return "Bakery";

        if (ContainsAny(name, description, MeatPoultryKeywords))
            return "Meat & Poultry";

        if (ContainsAny(name, description, SeafoodKeywords))
            return "Seafood";

        if (ContainsAny(name, description, FruitKeywords))
            return "Fruit";

        if (ContainsAny(name, description, VegetableKeywords))
            return "Vegetables";

        if (ContainsAny(name, description, FrozenFoodKeywords))
            return "Frozen Foods";

        if (ContainsAny(name, description, SnackKeywords))
            return "Snacks";

        if (ContainsAny(name, description, PantryKeywords))
            return "Pantry Staples";

        if (ContainsAny(name, description, HouseholdKeywords))
            return "Household & Cleaning";

        if (ContainsAny(name, description, PersonalCareKeywords))
            return "Personal Care";

        if (ContainsAny(name, description, PetCareKeywords))
            return "Pet Care";

        return "Other";
    }

    private bool ContainsAny(string text1, string text2, string[] keywords)
    {
        return keywords.Any(kw => 
            text1.Contains(kw, StringComparison.OrdinalIgnoreCase) || 
            text2.Contains(kw, StringComparison.OrdinalIgnoreCase)
        );
    }
}
# ExpenseTracker - Assignment:

**Denna uppgift ska utföras i par.**

## Funktionalitet
Skriv ett interaktivt konsolprogram som låter användaren hålla koll på sina utgifter. Programmet ska innehålla följande funktionalitet:

- Lägga till utgifter
    - Varje utgift består av namn, pris (inklusive moms) och kategori (`Utbildning`, `Böcker`, `Livsmedel`, eller `Övrigt`)
- Visa alla enskilda utgifter (enbart inklusive moms) och deras kategorier samt antalet utgifter och totalsumman (både inklusive och exklusive moms) av dessa utgifter
- Visa totalsumman (både inklusive och exklusive moms) av alla utgifter per kategori
- Ändra på en utgift
- Ta bort en enskild utgift
- Ta bort alla utgifter

### Regler för moms
Ert program ska hantera moms (*VAT* eller *value-added tax* på engelska) på följande sätt:

- När användaren matar in en ny utgift ska enbart totalpriset (inklusive moms) matas in.
- Programmet ska automatiskt lista ut momssatsen för en utgift baserat på kategorin:
    - `Utbildning`: 0%
    - `Böcker`: 6%
    - `Livsmedel`: 12%
    - `Övrigt`: 25%
- Visa samtliga värden med två decimaler.
    - Exempel: `12.34 kr`
    - Gör denna avrundning med `value.ToString("0.00")`.
    - Använd inte `Math.Round`, som ger fel resultat om värdet har färre än två siffror efter decimalen.

## Tester
Skriv automatiserade tester för en del av programmet, i form av unit-tester för en specifik metod.

- Ert program **måste** innehålla följande metod, där `Expense` är en klass som ni ska implementera själva:

        // Return the sum of all expenses in the specified list, with or without VAT based on the second parameter.
        public static decimal SumExpenses(List<Expense> expenses, bool includeVAT)
    
- Ert program **måste** innehålla åtminstone **3** tester (högst 10) som testar denna metod med olika intressanta scenarion.

- Denna metod **måste** också anropas från huvudprogrammet. Den får alltså inte användas *enbart* i testerna.

- Ni ska bara skriva **en** sådan metod. Ni får alltså inte skriva en separat `SumExpenses`-metod för exempelvis varje enskild utgiftskategori.

- Ni kan behöva avrunda metodens returvärde för att kunna testa den på ett pålitligt sätt. I detta fall går `Math.Round` bra att använda.

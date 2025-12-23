# AIAInteractions

AIAInteractions is a Blackbaud CRM feature pack containing interaction related customisations for Amnesty International Australia.

## Project Structure

- **AIAInteractions** - Base customisation specs
- **AIAInteractionsUIModel** - UI model specs and HTML layout

## Customisations

### Global Changes

- **Add Constituent Interaction (AIA)** - Bulk-creates interaction records for a selection of constituents. Supports relative dates (e.g., "15 days from today"), participant assignment, and site security filtering.

- **Add Interaction Attribute (AIA)** - Adds custom attributes to interaction records.

- **Delete Interaction Attribute (AIA)** - Removes custom attributes from interaction records.

- **Update Interaction Metadata (AIA)** - Bulk-updates interaction metadata fields for a given interaction selection including summary, status, dates, owner, contact method, category, and comments. Supports relative dates and only updates fields where a value is provided.

### Smart Fields

- **Interaction Dates (AIA)** - Returns the earliest or latest interaction date for a constituent filtered to the provided interaction selection or interaction criteria.

- **Interaction Appeals (AIA)** - Returns the appeal associated with a constituent's earliest or latest interaction filtered to the provided interaction selection or interaction criteria, optionally excluding interaction records with blank appeals. Requires the Blackbaud Interaction Marketing Effort extension.

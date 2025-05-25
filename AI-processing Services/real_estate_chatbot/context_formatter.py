def format_rich_context(properties, app_url):
    lines = []
    for prop in properties:
        url = f"{app_url}/PropertyDetails/{prop['PropertyId']}"
        lines.append(f"""
{prop['Name']} — {prop['Description']}
📍 {prop['Address']}, {prop['Locality']}, {prop['County']}, {prop['Country']}
🏠 {prop['NumberOfRooms']} rooms, {prop['NumberOfFloors']} floors, built in {prop['YearBuilt']}
📐 {prop['FloorArea']} m² floor area, {prop['PlotArea']} m² plot
💵 {prop['Price']} USD
🏷️ Type: {prop['PropertyType']}
🔗 [See Details]({url})
""")
    return "\n---\n".join(lines)
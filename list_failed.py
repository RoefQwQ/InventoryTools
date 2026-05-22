import json
with open('failed_runs.json') as f:
    data = json.load(f)
runs = data.get('workflow_runs', [])
for r in runs:
    print(f"{r['id']}  {r['name']:20}  {r['display_title']:30}  {r['created_at']}")

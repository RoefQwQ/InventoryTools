import json
import subprocess
import sys

with open('failed_runs.json') as f:
    data = json.load(f)

runs = data.get('workflow_runs', [])
total = len(runs)
print(f"Found {total} failed workflow runs to delete.")

for i, r in enumerate(runs, 1):
    run_id = r['id']
    name = r['name']
    title = r['display_title']
    url = f"https://api.github.com/repos/Critical-Impact/InventoryTools/actions/runs/{run_id}"
    print(f"[{i}/{total}] Deleting run {run_id} - {name} ({title})...")

    result = subprocess.run(
        ["curl", "-s", "-X", "DELETE", "-w", "\\nHTTP_CODE:%{http_code}", url],
        capture_output=True, text=True
    )

    output = result.stdout.strip()
    if "HTTP_CODE:" in output:
        body, _, code = output.partition("HTTP_CODE:")
        code = code.strip()
        if code == "204":
            print(f"  -> Deleted successfully (204)")
        elif code == "404":
            print(f"  -> Not found (404), may already be deleted")
        elif code == "403":
            print(f"  -> Forbidden (403), check permissions")
        else:
            print(f"  -> HTTP {code}, response: {body.strip()}")
    else:
        print(f"  -> Unexpected response: {output}")

print("Done.")

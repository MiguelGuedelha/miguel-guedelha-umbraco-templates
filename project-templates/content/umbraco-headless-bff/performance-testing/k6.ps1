pwsh -Command {
    $env:K6_WEB_DASHBOARD = "true";
    $env:K6_WEB_DASHBOARD_EXPORT = "k6-report.html";
    $env:K6_WEB_DASHBOARD_PERIOD = '5s'
    $env:K6_WEB_DASHBOARD_OPEN = 'true'
    k6 run k6.js
}

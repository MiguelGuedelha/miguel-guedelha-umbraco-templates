#!/usr/bin/env bash

K6_WEB_DASHBOARD=true K6_WEB_DASHBOARD_EXPORT=html-report.html K6_WEB_DASHBOARD_PERIOD='5s' K6_WEB_DASHBOARD_OPEN=true k6 run k6.js

import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
    reports = ['WebsiteAnalytics.mrt', 'HotelRevenue.mrt'];
  title = 'ClientApp';
  properties = { reportName: this.reports[0] };

  updateProps(reportName: string): void {
    this.properties = { reportName };
  }
}

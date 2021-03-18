import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'test-sidebar',
    templateUrl: 'sidebar.component.html'
})

export class SidebarComponent implements OnInit {
    @Input() title = 'Hello world';

    constructor() { }

    ngOnInit() { }
}
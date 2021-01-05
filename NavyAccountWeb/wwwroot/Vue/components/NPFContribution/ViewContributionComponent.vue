<template>
    <!-- WRAPPER -->
    <div>

        <div class="card-body">
            <table id="datatables-buttons" class="table table-striped table-responsive" style="width:100%">
                <thead>
                    <tr>
                        <th>FundType</th>
                        <th>OS</th>
                        <th>SM</th>
                        <th>AB</th>
                        <th>LS</th>
                        <th>PO</th>
                        <th>WO</th>
                        <th>MWO</th>
                        <th>NWO</th>
                        <th>S/Lt</th>
                        <th>Lt</th>
                        <th>Lt/Cdr</th>
                        <th>Cdr</th>
                        <th>Capt</th>
                        <th>Cdre</th>
                        <th>R/Adm</th>
                        <th>V/Adm</th>
                        <th>Adm</th>
                        <th>Contract</th>
                        <th>Retired</th>
                      
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="fundrate in contributionList">
                        <td>{{ fundrate.fundtype }}</td>
                        <td>{{ fundrate.amount01 }}</td>
                        <td> {{ fundrate.amount02 }}</td>
                        <td> {{ fundrate.amount03 }}</td>
                        <td> {{ fundrate.amount04 }}</td>
                        <td> {{ fundrate.amount05 }}</td>
                        <td> {{ fundrate.amount06 }}</td>
                        <td> {{ fundrate.amount07 }}</td>
                        <td> {{ fundrate.amount08 }}</td>
                        <td> {{ fundrate.amount09 }}</td>
                        <td> {{ fundrate.amount10 }}</td>
                        <td> {{ fundrate.amount11 }}</td>
                        <td> {{ fundrate.amount12 }}</td>
                        <td> {{ fundrate.amount13 }}</td>
                        <td> {{ fundrate.amount14 }}</td>
                        <td> {{ fundrate.amount15 }}</td>
                        <td> {{ fundrate.amount16 }}</td>
                        <td> {{ fundrate.amount17 }}</td>
                        <td> {{ fundrate.amount18 }}</td>
                        <td> {{ fundrate.amount19 }}</td>
                       
                        <td><button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(fundrate)">Edit</button></td>
                    </tr>
                </tbody>

            </table>
        </div>

    </div>

    <!-- END WRAPPER -->
</template>

<script>
export default {

            data() {
                return {
                contributionList:null
                };
            },
        created() {
            this.$store.state.objectToUpdate = null;
        },

    mounted () {
        axios
            .get('/api/NpfContribution/getAllNPFContribution')
            .then(response => (this.contributionList = response.data))
     },
     methods: {

         processRetrieve: function (fundrate) {

            this.$store.state.objectToUpdate = fundrate;
        }
    }

};
</script>
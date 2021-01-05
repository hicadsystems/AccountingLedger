<template>
    	<!-- WRAPPER -->
    <div>
        <div class="card-body">
            
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Service Number</th>
                        <th>Full Name</th>
                        <th>Rank </th>
                        <th>Gender</th>
                        <th>Account No</th>
                        <!--<th>Beneficiary</th>-->
                        <th>Update</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="personel in personelList">
                        <td>{{ personel.svC_NO }}</td>
                        <td>{{ personel.firstName }}  {{ personel.middleName }}</td>
                        <td>{{ personel.rank }}</td>
                        <td>{{ getAppropriateGender(personel.sex) }}</td>
                        <td>{{ personel.accountno }}</td>
                        <!--<td><button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(mainact)">Beneficiary</button></td>-->
                        <td><a type="button" :href="'CreatePerson?id='+personel.personID" class="btn btn-submit btn-primary">Edit</a></td>
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
            PersonID:0,
        personelList:null
        };
    },
     created() {
            this.$store.state.objectToUpdate = null;
    },
    mounted () {
        axios
        .get('/api/PersonAPI/getAllInactivePersons')
        .then(response => (this.personelList = response.data))
     },
     methods: {
        
 
         processRetrieve: function (mainAccount) {

            //this.$store.state.objectToUpdate = mainAccount;
         },

         getAppropriateGender: function (gender) {
             if (gender == 'M')
                 return 'Male';
             if (gender == 'F')
                 return 'Female';
         }
    }
    
};
</script>